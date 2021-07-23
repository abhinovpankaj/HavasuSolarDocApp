using DocumentFormat.OpenXml.Packaging;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NavigationView.ViewModels
{
    public class OrderRepoViewModel:ViewModelBase
    {
        private string _projectName;
        public string ProjectName
        {
            get
            {
                return _projectName;
            }
            set
            {
                if (value!=_projectName)
                {
                    _projectName = value;
                    RaisePropertyChanged("ProjectName");
                }
            }
        }

        public string ReferralPaidTo { get; set; }
        public string RebatePaidTo { get; set; }

        public string ImagePath { get; set; }

        public TimeSpan? InstallTime { get; set; }

        public DateTimeOffset? InstallDate { get; set; }

        public RelayCommand CreateDocCommand
        {
            get
            {
                return new RelayCommand(CreateRebateDoc, true);
            }
        }

        private async void CreateRebateDoc()
        {
            Windows.Storage.StorageFolder folder = Windows.Storage.KnownFolders.PicturesLibrary;
            
            //Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            
            Windows.Storage.StorageFile file = await folder.GetFileAsync("Rebate_Referral Form.docx");
            
            Stream randomAccessStream = await file.OpenStreamForWriteAsync();
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(randomAccessStream, true))
            {
                string docText = null;
                using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
                {
                    docText = sr.ReadToEnd();
                }

                Regex regexText = new Regex("ProjectName");
                docText = regexText.Replace(docText, ProjectName);
                Regex regexText1 = new Regex("ReferralPaidTo");
                docText = regexText1.Replace(docText, ReferralPaidTo);
                Regex regexText2 = new Regex("RebatePaidTo");
                docText = regexText2.Replace(docText, RebatePaidTo);
                //docText.Replace("ProjectName", ProjectName);
                //docText.Replace("ReferralPaidTo", ReferralPaidTo);
                //docText.Replace("RebatePaidTo", RebatePaidTo);
                using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
                {
                    sw.Write(docText);
                }
            }
            randomAccessStream.Close();

           await Windows.System.Launcher.LaunchFileAsync(file);
        }
    }
}
