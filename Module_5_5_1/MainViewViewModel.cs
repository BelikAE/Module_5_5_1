using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Prism.Commands;
using MyRevitAPILibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Module_5_5_1
{
    public class MainViewViewModel
    {
        private ExternalCommandData _commandData;

        public DelegateCommand SelectPipes { get; } 
        public DelegateCommand SelectWalls { get; }
        public DelegateCommand SelectDoors { get; }
        
        public MainViewViewModel(ExternalCommandData commandData)
        {
            _commandData = commandData;
            SelectPipes = new DelegateCommand(OnSelectPipesCommand);
            SelectWalls = new DelegateCommand(OnSelectWallsCommand);
            SelectDoors = new DelegateCommand(OnSelectDoorsCommand);
        }


        public event EventHandler HideRequest; //Эвент скрытия окна
        private void RaiseHideRequest()
        {
            HideRequest?.Invoke(this, EventArgs.Empty); 
        }

        public event EventHandler ShowRequest; //Эвент показа окна
        private void RaiseShowRequest()
        {
            ShowRequest?.Invoke(this, EventArgs.Empty);
        }

        private void OnSelectPipesCommand()
        {
            RaiseHideRequest(); //Скрываем окно

            var pipes = SelectionUtils.SelectionPipes(_commandData);

            TaskDialog.Show("Сообщение", $"Колличетсво труб: {pipes.Count}");

            RaiseShowRequest(); //Показывает окно
        }

        private void OnSelectWallsCommand()
        {

            RaiseHideRequest(); //Скрываем окно
            var walls = SelectionUtils.SelectionWalls(_commandData);
            double sumVoluem = walls.Sum(x => x.get_Parameter(BuiltInParameter.HOST_VOLUME_COMPUTED).AsDouble());

            TaskDialog.Show("Сообщение", $"Объем стен: {Math.Round(UnitUtils.ConvertFromInternalUnits(sumVoluem, UnitTypeId.CubicMeters),2)}");
            RaiseShowRequest(); //Показывает окно
        }

        private void OnSelectDoorsCommand()
        {
            RaiseHideRequest(); //Скрываем окно

            var doors = SelectionUtils.SelectionDoors(_commandData);

            TaskDialog.Show("Сообщение", $"Колличество дверей: {doors.Count}");

            RaiseShowRequest(); //Показывает окно
        }

    }
}
