﻿using MMAPredictor.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Globalization;
using MMAPredictor.DataScrapper.Interface;
using MMAPredictor.DataScrapper.Utilities;

namespace MMAPredictor.DataScrapper.Service
{
    public class UFCFighterScrapperService : IUFCFighterScrapperService
    {
        public async Task<FighterDTO?> ScrapFighterPageAsync(string? url, string? path)
        {
            try
            {
                HtmlDocument htmlDoc = null;
                if (!string.IsNullOrEmpty(path))
                {
                    htmlDoc = new HtmlDocument();
                    htmlDoc.Load(path);
                }
                else
                {
                    htmlDoc = await new HtmlWeb().LoadFromWebAsync(url);
                }

                FighterDTO? fighterDto = InitializeFighterDto(htmlDoc);
                if (fighterDto == null)
                {
                    return null;
                }

                FillFighterRecord(htmlDoc, fighterDto);

                FillFighterPhysique(htmlDoc, fighterDto);

                FillCareerStatistics(htmlDoc, fighterDto);

                return fighterDto;
            }
            catch
            {
                return null;
            }
        }

        private FighterDTO? InitializeFighterDto(HtmlDocument htmlDoc)
        {
            if (htmlDoc is null)
            { 
                return null; 
            }
            string? fighterName = ScrappingUtils.SelectNode<string>(htmlDoc, "//body/section[@class='b-statistics__section_details']//span[@class='b-content__title-highlight']");
            if (!string.IsNullOrEmpty(fighterName))
            {
                return new FighterDTO()
                {
                    Name = fighterName
                };
            }
            return null;
        }

        private bool FillFighterRecord(HtmlDocument htmlDoc, FighterDTO fighterDto)
        {
            string? record = ScrappingUtils.SelectNode<string>(htmlDoc, "//body/section[@class='b-statistics__section_details']//span[@class='b-content__title-record']");
            if (!string.IsNullOrEmpty(record))
            {
                Regex recordRegex = new Regex("^record:\\s+(?<wins>\\d+)-(?<losses>\\d+)-(?<draws>\\d+)", RegexOptions.IgnoreCase);
                if (recordRegex.IsMatch(record))
                {
                    var match = recordRegex.Matches(record)[0];
                    fighterDto.NbWins = int.Parse(match.Groups["wins"].Value);
                    fighterDto.NbLoss = int.Parse(match.Groups["losses"].Value);
                    fighterDto.NbDraws = int.Parse(match.Groups["draws"].Value);

                    return true;
                }
            }
            return false;
        }

        private void FillFighterPhysique(HtmlDocument htmlDoc, FighterDTO fighterDto)
        {
            var nodeCollection = ScrappingUtils.SelectListNodes(htmlDoc, "(//body/section[@class='b-statistics__section_details']//div[contains(@class, 'b-fight-details')]/div)[1]//li");
            foreach(var n in nodeCollection)
            {
                if (n.StartsWith("HEIGHT:", StringComparison.InvariantCultureIgnoreCase))
                {
                    var value = n.ToLower().Replace("height:", "");
                    Regex reg = new Regex("(?<feet>\\d+)'\\s+(?<inches>\\d+)\"");
                    if (reg.IsMatch(value))
                    {
                        var match = reg.Matches(value)[0];
                        var feet = int.Parse(match.Groups["feet"].Value);
                        var inches = int.Parse(match.Groups["inches"].Value);
                        var meters = (12 * feet + inches) * 2.54;
                        fighterDto.Height = meters;
                    }
                }
                else if (n.StartsWith("WEIGHT:", StringComparison.InvariantCultureIgnoreCase))
                {
                    var value = n.ToLower().Replace("weight:", "");
                    Regex reg = new Regex("(?<pounds>\\d+)\\s+lbs");
                    if (reg.IsMatch(value))
                    {
                        var match = reg.Matches(value)[0];
                        var pounds = double.Parse(match.Groups["pounds"].Value);
                        fighterDto.Weight = pounds * 0.4536;
                    }
                }
                else if (n.StartsWith("REACH:", StringComparison.InvariantCultureIgnoreCase))
                {
                    var value = n.ToLower().Replace("reach:", "");
                    Regex reg = new Regex("\\s+(?<inches>\\d+)\"");
                    if (reg.IsMatch(value))
                    {
                        var match = reg.Matches(value)[0];
                        var inches = double.Parse(match.Groups["inches"].Value);
                        fighterDto.Reach = inches * 2.54;
                    }
                }
                else if (n.StartsWith("STANCE:", StringComparison.InvariantCultureIgnoreCase))
                {
                    fighterDto.Stance = n.ToLower().Replace("stance:", "").Trim();
                }
                else if (n.StartsWith("DOB:", StringComparison.InvariantCultureIgnoreCase))
                {
                    var value = n.ToLower().Replace("dob:", "");
                    if (DateTime.TryParse(value, CultureInfo.GetCultureInfo("en-US").DateTimeFormat, out DateTime dob))
                    {
                        fighterDto.DateOfBirth = dob;
                    }
                }
            }
        }

        private void FillCareerStatistics(HtmlDocument htmlDoc, FighterDTO fighterDto)
        {
            Regex regexValue = new Regex("(?<integer>\\d+)\\.(?<decimal>\\d+)");
            Regex regexPercentage = new Regex("(?<value>\\d+)%");
            double? ExtractDecimalValue(string value)
            {
                if (regexValue.IsMatch(value))
                {
                    var match = regexValue.Matches(value)[0];
                    var integer = int.Parse(match.Groups["integer"].Value);
                    var sDec = match.Groups["decimal"].Value;
                    var dec = decimal.Parse(sDec);
                    decimal res = integer + dec / (decimal)Math.Pow(10, sDec.Length);
                    return (double)res;
                }
                return null;
            }

            double? ExtractPercentageValue(string value)
            {
                if (regexPercentage.IsMatch(value))
                {
                    var match = regexPercentage.Matches(value)[0];
                    var perc = double.Parse(match.Groups["value"].Value);
                    return perc;
                }
                return null;
            }

            var nodeCollection = ScrappingUtils.SelectListNodes(htmlDoc, "(//body/section[@class='b-statistics__section_details']//div[contains(@class, 'b-fight-details')]/div)[2]//div[contains(@class,'b-list__info-box-left')]/div//li");
            foreach (var n in nodeCollection)
            {
                if (n.StartsWith("SLpM:"))
                {
                    var value = n.ToLower().Replace("slpm:", "");
                    fighterDto.StrikesLandedByMinute = ExtractDecimalValue(value.Trim(' ', '\n'));
                }
                else if (n.StartsWith("Str. Acc.:"))
                {
                    var value = n.ToLower().Replace("str. acc:", "");
                    fighterDto.StrikesAccuracy = ExtractPercentageValue(value);
                }
                else if (n.StartsWith("SApM:"))
                {
                    var value = n.ToLower().Replace("sapm:", "");
                    fighterDto.StrikesAbsorbedByMinute = ExtractDecimalValue(value);
                }
                else if (n.StartsWith("Str. Def:"))
                {
                    var value = n.ToLower().Replace("str. def:", "");
                    fighterDto.StrikingDefenceAccuracy = ExtractPercentageValue(value);
                }
                else if (n.StartsWith("TD Avg.:"))
                {
                    var value = n.ToLower().Replace("td avg.:", "");
                    fighterDto.TakedownAverage = ExtractDecimalValue(value);
                }
                else if (n.StartsWith("TD Acc.:"))
                {
                    var value = n.ToLower().Replace("td acc.:", "");
                    fighterDto.TakedownAccuracy = ExtractPercentageValue(value);
                }
                else if (n.StartsWith("TD Def.:"))
                {
                    var value = n.ToLower().Replace("td def.:", "");
                    fighterDto.TakedownDefenceAccuracy = ExtractPercentageValue(value);
                }
                else if (n.StartsWith("Sub. Avg.:"))
                {
                    var value = n.ToLower().Replace("sub. avg.:", "");
                    fighterDto.SubmissionsAverage = ExtractDecimalValue(value);
                }
            }
        }        
    }
}
