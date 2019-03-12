using AirspaceUsage.AirspaceMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AirspaceUsage.Settings
{
    /// <summary>
    /// Interaction logic for WarningAreaSettings.xaml
    /// </summary>
    public partial class WarningAreaSettings : UserControl
    {
        private WarningAreaGroup currentGroup;
        private bool newGroup;

        public WarningAreaSettings()
        {
            InitializeComponent();
        }

        private void NewGroup(object sender, RoutedEventArgs e)
        {
            currentGroup = new WarningAreaGroup() { GroupName = "New Group", GroupDescription = "Description" };
            newGroup = true;
        }

        private void GroupDescriptionChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void GroupTextChanged(object sender, TextChangedEventArgs e)
        {
            currentGroup.GroupName = groupName.Text;
            warningAreasComboBox.InvalidateVisual();
        }

        private void WarningAreasDropDownClosed(object sender, EventArgs e)
        {
            if(newGroup)
            {
                warningAreasComboBox.Items.Add(currentGroup);
                warningAreasComboBox.SelectedItem = currentGroup;
                newGroup = false;
            }
        }

        private void WarningAreasSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(warningAreasComboBox.SelectedIndex != 0)
            {
                currentGroup = (WarningAreaGroup)warningAreasComboBox.SelectedItem;
                groupName.Text = currentGroup.GroupName;
                groupDescription.Text = currentGroup.GroupDescription;
            }
        }
    }
}
