using CsvHelper;
using ERPTest3.Models;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ERPTest3.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public DelegateCommand OpenNCommand { get; private set; }
        public DelegateCommand OpenPCommand { get; private set; }
        public DelegateCommand OpenSCommand { get; private set; }
        public DelegateCommand ProcessCommand { get; private set; }

        public Random rnd { get; set; }

        private ObservableCollection<Article> lstArticle;
        public ObservableCollection<Article> LstArticle
        {
            get { return lstArticle; }
            set { SetProperty(ref lstArticle, value); }
        }

        private ObservableCollection<Stock> lstStock;
        public ObservableCollection<Stock> LstStock
        {
            get { return lstStock; }
            set { SetProperty(ref lstStock, value); }
        }

        private ObservableCollection<Prevision> lstPrevision;
        public ObservableCollection<Prevision> LstPrevision
        {
            get { return lstPrevision; }
            set { SetProperty(ref lstPrevision, value); }
        }

        private ObservableCollection<Resultat> lstResult;
        public ObservableCollection<Resultat> LstResult
        {
            get { return lstResult; }
            set { SetProperty(ref lstResult, value); }
        }

        private bool isComputePossible;
        public bool IsComputePossible
        {
            get { return isComputePossible; }
            set { SetProperty(ref isComputePossible, value); }
        }

        public OpenFileDialog _openFileDlg { get; set; }

        public CsvReader _csvReader { get; set; }

        public MainWindowViewModel()
        {
            OpenNCommand = new DelegateCommand(OpenNFunc);
            OpenPCommand = new DelegateCommand(OpenPFunc);
            OpenSCommand = new DelegateCommand(OpenSFunc);
            ProcessCommand = new DelegateCommand(ComputeFunc);
            rnd = new Random();
            _openFileDlg = new OpenFileDialog();
            LstArticle = new ObservableCollection<Article>();
            LstStock = new ObservableCollection<Stock>();
            LstPrevision = new ObservableCollection<Prevision>();
            LstResult = new ObservableCollection<Resultat>();
            _openFileDlg.Filter = "CSV (.csv)|*.csv";
            _openFileDlg.Multiselect = false;
            IsComputePossible = false;
        }

        private void ComputeFunc()
        {
            GenerateFakeResult(8);
        }

        private void CheckIfComputeIsPossible()
        {
            if (LstArticle.Count != 0 && LstPrevision.Count != 0 && LstStock.Count != 0) IsComputePossible = true;
            else IsComputePossible = false;
        }

        private void OpenSFunc()
        {
            if(_openFileDlg.ShowDialog()== true)
            {
                try
                {
                    _csvReader = new CsvReader(File.OpenText(_openFileDlg.FileName));
                    _csvReader.Configuration.Delimiter = ";";
                    LstStock = new ObservableCollection<Stock>(_csvReader.GetRecords<Stock>());
                    MessageBox.Show("import des stock terminé");
                    CheckIfComputeIsPossible();
                }
                catch (Exception ex)
                {
                    if (!_openFileDlg.FileName.Contains("Stock")) MessageBox.Show("Vous n'avez peut être pas ouvert le bon fichier");
                    else { MessageBox.Show("la lecture a échoué : code erreur " + ex.ToString()); }
                }
            }
        }


        private void GenerateFakeResult(int nb)
        {
            for(int i =0; i < nb; i++)
            {
                LstResult.Add(new Resultat() { BB = rnd.Next(100,200) , BN = rnd.Next(100, 200), L = rnd.Next(0, 100), OL = rnd.Next(0, 100), S = rnd.Next(0, 100) , Sugg = rnd.Next(0, 100) , Semaine = "S"+(i+1).ToString()});
            }
        }

        private void OpenPFunc()
        {
            if (_openFileDlg.ShowDialog() == true)
            {
                try
                {
                    _csvReader = new CsvReader(File.OpenText(_openFileDlg.FileName));
                    _csvReader.Configuration.Delimiter = ";";
                    LstPrevision = new ObservableCollection<Prevision>(_csvReader.GetRecords<Prevision>());
                    MessageBox.Show("import des previsions terminé");
                    CheckIfComputeIsPossible();
                }
                catch (Exception ex)
                {
                    if (!_openFileDlg.FileName.Contains("Prevision")) MessageBox.Show("Vous n'avez peut être pas ouvert le bon fichier");
                    else { MessageBox.Show("la lecture a échoué : code erreur " + ex.ToString()); }
                }
            }
        }

        private void OpenNFunc()
        {
            if (_openFileDlg.ShowDialog() == true)
            {
                try
                {
                    _csvReader = new CsvReader(File.OpenText(_openFileDlg.FileName));
                    _csvReader.Configuration.Delimiter = ";";
                    LstArticle = new ObservableCollection<Article>(_csvReader.GetRecords<Article>());
                    MessageBox.Show("import de la nomenclature terminé");
                    CheckIfComputeIsPossible();
                }
                catch(Exception ex)
                {
                    if (!_openFileDlg.FileName.Contains("Nomenclature")) MessageBox.Show("Vous n'avez peut être pas ouvert le bon fichier");
                    else { MessageBox.Show("la lecture a échoué : code erreur " + ex.ToString()); }
                }
               
                
            }
        }
    }
}
