using PMS.BL.Core;
using PMS.BL.Model;
using PMS.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PMS.Core
{
    public class CreateSectionsAndStagesCommand: ICommand
    {
        public CreateSectionsAndStagesCommand()
        {

        }

        public event EventHandler CanExecuteChanged;


        public bool CanExecute(object parameter)
        {
            bool ret = true;
            if (parameter is CreateProjectViewModel)
            {
                CreateProjectViewModel componentInfoVM = parameter as CreateProjectViewModel;

                if (componentInfoVM == null)
                {
                    ret = false;
                    MessageBox.Show("Ошибка");
                }
            }

            return ret;


        }

        public void Execute(object parameter)
        {
            if (parameter is CreateProjectViewModel)
            {
                CreateProjectViewModel createProjectVM = parameter as CreateProjectViewModel;

                if (createProjectVM != null)
                {
                    try
                    {
                        if (!createProjectVM.ResetSRW)
                        {
                            try
                            {
                                createProjectVM.SRWsection = createProjectVM.Project.AddSection(SectionEnum.SRW, createProjectVM.SRWCipher);
                                createProjectVM.SRWsection.InitSRWStages(new List<bool>() { createProjectVM.OneSRW, createProjectVM.TwoSRW, createProjectVM.ThreeSRW, createProjectVM.FourSRW });
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Не удалось добавить секцию. " + ex.Message);
                                throw;
                            }
                        }

                        if (!createProjectVM.ResetEDW)
                        {
                            try
                            {
                                createProjectVM.EDWsection = createProjectVM.Project.AddSection(SectionEnum.EDW, createProjectVM.EDWCipher);
                                createProjectVM.EDWsection.InitEDWStages(new List<string>() { createProjectVM.SelectedStartEDWContent, createProjectVM.SelectedMiddleEDWContent, createProjectVM.SelectedEndEDWContent });
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Не удалось добавить секцию. " + ex.Message);
                                throw;
                            }
                        }

                        MainViewModel mainViewModel = createProjectVM.ParentMain;
                        AllProjectsViewModel allProjectsViewModel = new AllProjectsViewModel(createProjectVM.Parent.Employee)
                        {
                            Parent = mainViewModel
                        };
                        mainViewModel.AllProjectsVM = allProjectsViewModel;
                        mainViewModel.CurrentView = mainViewModel.AllProjectsVM;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
