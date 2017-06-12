using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSVReader
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var FilePath = ConfigurationManager.AppSettings["FilePath"];

            new ManipulateFile().CSVInput(FilePath);
        }
    }

    public class ManipulateFile
    {
        public List<Profile> _Profile { get; set; }

        public void CSVInput(string csvlocation)
        {
            string startupPath = System.IO.Directory.GetCurrentDirectory();
            try
            {

                var lines = File.ReadAllLines(csvlocation).Select(a => a.Split(';'));
                List<Profile> _Profile = new List<Profile>();

                if (lines != null)
                {
                    foreach (var strLine in lines)
                    {
                        var arrLineItems = strLine[0].ToString().Split(',');

                        if (!arrLineItems[0].Equals("firstname", StringComparison.OrdinalIgnoreCase))
                        {
                            _Profile.Add(new Profile
                            {
                                Firstname = arrLineItems[0],
                                Lastname = arrLineItems[1],
                                Address = arrLineItems[2],
                                ContactNr = arrLineItems[3]
                            });
                        }
                    }
                    foreach (var _FrequencyProfile in _Profile)
                    {
                        _FrequencyProfile.frequencynumber = _Profile.Count(str => str.Firstname.Contains(_FrequencyProfile.Firstname) && str.Lastname.Contains(_FrequencyProfile.Lastname));
                    }
                }
                var orderedListByFrequency = _Profile.Select(s => s).OrderByDescending(o => o.frequencynumber);


                using (var file = File.CreateText("C:\\Users\\shomishanang\\Downloads\\frequency.csv"))
                {
                    foreach (var arr in orderedListByFrequency)
                    {
                        file.WriteLine(string.Join(",", arr.Lastname + ',' + arr.frequencynumber));
                    }
                }

                var orderedListByCity = _Profile.Select(s => s).OrderBy(o => o.Address.Split(' ')[1]);

                using (var file = File.CreateText("C:\\Users\\shomishanang\\Downloads\\city.csv"))
                {
                    foreach (var arr in orderedListByCity)
                    {
                        file.WriteLine(string.Join(",", arr.Address));
                    }
                }
            }
            catch (Exception e)
            {
                var msg = e.Message;
            }
        }       
    }
    public class Profile
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string ContactNr { get; set; }
        public int frequencynumber { get; set; }
    }
}

