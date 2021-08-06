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

        private  void CreateRebateDoc()
        {
           // Windows.Storage.StorageFolder folder = Windows.Storage.KnownFolders.PicturesLibrary;
            
           // //Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            
           // Windows.Storage.StorageFile file = await folder.GetFileAsync("Rebate_Referral Form.docx");
            
           // Stream randomAccessStream = await file.OpenStreamForWriteAsync();
           // using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(randomAccessStream, true))
           // {
           //     string docText = null;
           //     using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
           //     {
           //         docText = sr.ReadToEnd();
           //     }

           //     Regex regexText = new Regex("ProjectName");
           //     docText = regexText.Replace(docText, ProjectName);
           //     Regex regexText1 = new Regex("ReferralPaidTo");
           //     docText = regexText1.Replace(docText, ReferralPaidTo);
           //     Regex regexText2 = new Regex("RebatePaidTo");
           //     docText = regexText2.Replace(docText, RebatePaidTo);
           //     //docText.Replace("ProjectName", ProjectName);
           //     //docText.Replace("ReferralPaidTo", ReferralPaidTo);
           //     //docText.Replace("RebatePaidTo", RebatePaidTo);
           //     using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
           //     {
           //         sw.Write(docText);
           //     }
           // }
           // randomAccessStream.Close();

           //await Windows.System.Launcher.LaunchFileAsync(file);
        }

        private double _moduleCount;
        public double SystemSizeWatt
        {
            get
            {
                return _moduleCount*340;
            }

            
        }
        public double ModuleCount
        {
            get
            {
                return _moduleCount;
            }
            set
            {
                if (value!=_moduleCount)
                {
                    _moduleCount = value;
                    RaisePropertyChanged("ModuleCount");
                    RaisePropertyChanged("SystemSizeWatt");
                    RaisePropertyChanged("RepCommision");
                }
            }
        }
        private double _baseRedLine;
        public double BaseRedLine
        {
            get
            {
                return _baseRedLine;
            }
            set
            {
                if (value != _baseRedLine)
                {
                    _baseRedLine = value;
                    RaisePropertyChanged("BaseRedLine");
                    RaisePropertyChanged("SystemSizeWatt");
                    RedLineCost = _baseRedLine * SystemSizeWatt;
                    RaisePropertyChanged("RedLineCost");
                }
            }
        }
        private double _costperWatt;
        public double CostPerWatt
        {
            get
            {
                return _costperWatt;
            }
            set
            {
                if (value != _costperWatt)
                {
                    _costperWatt = value;
                    RaisePropertyChanged("CostPerWatt");
                    RaisePropertyChanged("SystemSizeWatt");
                    PurchaseCost = _costperWatt * SystemSizeWatt;
                    RaisePropertyChanged("PurchaseCost");
                }
            }
        }
        public double Trenching { get; set; }
        public double PurchaseCost { get; set; }
        public double RedLineCost { get; set; }
        public double NickleValue { get; set; }
        public double TripCharge { get; set; }
        public double MPU { get; set; }
        public double DerateMain { get; set; }
        public double FlatRoofAttachmnetValue { get; set; }
        public double SmallSystemSize { get; set; }
        public double DedicatedTransformer { get; set; }
        public double MECUtilityFees { get; set; }
        private double _nickleUp;
        public double NickleUp
        {
            get
            {
                return _nickleUp;
            }
            set
            {
                if (value != _nickleUp)
                {
                    _nickleUp = value;
                    RaisePropertyChanged("NickleUp");
                    NickleValue = _nickleUp * SystemSizeWatt;
                    RaisePropertyChanged("NickleValue");
                    RaisePropertyChanged("RepCommision");
                }
            }
        }

        private double _roofperAttach;
        public double RoofperAttach
        {
            get
            {
                return _roofperAttach;
            }
            set
            {
                if (value != _roofperAttach)
                {
                    _roofperAttach = value;
                    RaisePropertyChanged("RoofperAttach");
                    FlatRoofAttachmnetValue = _roofperAttach * ModuleCount;
                    
                    RaisePropertyChanged("FlatRoofAttachmnetValue");

                }
            }
        }

        private double _redlineValue;
        public double RedlineValue
        {
            get
            {
                return TripCharge + MPU + DerateMain + FlatRoofAttachmnetValue + SmallSystemSize + DedicatedTransformer + MECUtilityFees;
            }
            //set
            //{
            //    if (value != _redlineValue)
            //    {
            //        _redlineValue = value;
            //        RaisePropertyChanged("RedlineValue");
                    
            //        RaisePropertyChanged("FlatRoofAttachmnetValue");

            //    }
            //}
        }

        private double _dealerPerc;
        public double DealerPerc
        {
            get
            {
                return _dealerPerc;
            }
            set
            {
                if (value!=_dealerPerc)
                {
                    _dealerPerc = value;
                    RaisePropertyChanged("DealerPerc");
                    DealerFee = _dealerPerc * RedlineValue;
                }
            }
        }
        private double _acq;
        public double Acquisiton
        {
            get
            {
                return _acq;
            }
            set
            {
                if (value != _acq)
                {
                    _acq = value;
                    RaisePropertyChanged("Acquisiton");
                    AcquisitionCost = _acq * SystemSizeWatt;
                    RaisePropertyChanged("AcquisitionCost");
                }
            }
        }
        private double _qFee;
        public double QFee
        {
            get
            {
                return _qFee;
            }
            set
            {
                if (value != _qFee)
                {
                    _qFee = value;
                    RaisePropertyChanged("QFee");
                    QuotationFees = _qFee * SystemSizeWatt;
                    RaisePropertyChanged("QuotationFees");
                }
            }
        }
        private double _lCost;
        public double LCost
        {
            get
            {
                return _lCost;
            }
            set
            {
                if (value != _lCost)
                {
                    _lCost = value;
                    RaisePropertyChanged("LCost");
                    LeadCost = _lCost * SystemSizeWatt;
                    RaisePropertyChanged("LeadCost");
                }
            }
        }
        public double DealerFee { get; private set; }
        public double AcquisitionCost { get; private set; }
        public double QuotationFees { get; private set; }
        public double LeadCost { get; private set; }

        public double RepCommision
        {
            get
            {
                return (0.2 + NickleUp) * SystemSizeWatt;
            }
        }
        public double AdvanceCost
        {
            get
            {
                return (0.5 ) * SystemSizeWatt;
            }
        }

        public double Balance
        {
            get
            {
                return RepCommision-AdvanceCost;
            }
        }

    }
}
