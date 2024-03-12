using EvernoteClone.ViewModel;
using EvernoteClone.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EvernoteClone.View
{
    /// <summary>
    /// Lógica interna para NotesWindow.xaml
    /// </summary>
    public partial class NotesWindow : Window
    {
        NotesViewModel viewModel;
        public NotesWindow()
        {
            InitializeComponent();
            var fontFamilies = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            fontFamillyComboBox.ItemsSource = fontFamilies;

            viewModel = Resources["vm"] as NotesViewModel;
            viewModel.SelectedNoteChanged += ViewModel_SelectedNoteChanged;

            List<Double> fontSize = new List<Double>() { 8,9,10,11,12,14,16,28,48,72};
            fontSizeComboBox.ItemsSource = fontSize;
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            if (string.IsNullOrEmpty(App.UserId))
            {
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();

                viewModel.getNotebooks();
            }
        }

        private void ViewModel_SelectedNoteChanged(object sender, EventArgs e)
        {
            contentRichTextBox.Document.Blocks.Clear();
            if(viewModel.SelectedNote != null)
            {
                if (!string.IsNullOrEmpty(viewModel.SelectedNote.FileLocation))
                {
                    FileStream fileStream = new FileStream(viewModel.SelectedNote.FileLocation, FileMode.Open);
                    var content = new TextRange(contentRichTextBox.Document.ContentStart, contentRichTextBox.Document.ContentEnd);
                    content.Load(fileStream, DataFormats.Rtf);
                    fileStream.Close();
                }
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuItem_Click_Logout(object sender, RoutedEventArgs e)
        {
            //ToDo: Redirecionar para a tela de login.
        }

        private void SpeechButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void contentRichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int ammounfCharacteres = (new TextRange(contentRichTextBox.Document.ContentStart, contentRichTextBox.Document.ContentEnd)).Text.Length;
            statusTextBlock.Text = $"Docoument length: {ammounfCharacteres} characters";
        }

        private void boldButton_Click(object sender, RoutedEventArgs e)
        {
            bool IsButtonChecked= (sender as ToggleButton).IsChecked ?? false;
            
            if(IsButtonChecked)
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
            else
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Normal);
        }

        private void contentRichTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var seletectedWeight = contentRichTextBox.Selection.GetPropertyValue(FontWeightProperty);
            boldButton.IsChecked = (seletectedWeight != DependencyProperty.UnsetValue) && seletectedWeight.Equals(FontWeights.Bold);

            var selectedStyle = contentRichTextBox.Selection.GetPropertyValue(Inline.FontStyleProperty);
            boldButton.IsChecked = (selectedStyle != DependencyProperty.UnsetValue) && selectedStyle.Equals(FontStyles.Italic);

            var selectedDecoration = contentRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            boldButton.IsChecked = (selectedDecoration != DependencyProperty.UnsetValue) && selectedDecoration.Equals(TextDecorations.Underline);

            fontFamillyComboBox.SelectedItem = contentRichTextBox.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            fontSizeComboBox.Text = (contentRichTextBox.Selection.GetPropertyValue(Inline.FontSizeProperty)).ToString();
        }

        private void italicButton_Click(object sender, RoutedEventArgs e)
        {
            bool IsButtonChecked = (sender as ToggleButton).IsChecked ?? false;

            if (IsButtonChecked)
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Italic);
            else
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Normal);
        }

        private void underlineButton_Click(object sender, RoutedEventArgs e)
        {
            bool IsButtonChecked = (sender as ToggleButton).IsChecked ?? false;

            if(IsButtonChecked)
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            else
            {
                TextDecorationCollection textDecorations;
                (contentRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty) as TextDecorationCollection)
                    .TryRemove(TextDecorations.Underline, out textDecorations);
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, textDecorations);
            }
        }

        private void fontFamillyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (fontFamillyComboBox.SelectedItem != null)
            {
                contentRichTextBox.Selection.ApplyPropertyValue(FontFamilyProperty, fontFamillyComboBox.SelectedItem);
            }
        }

        private void fontSizeComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (fontSizeComboBox.SelectedItem != null)
            {
                contentRichTextBox.Selection.ApplyPropertyValue(FontSizeProperty, fontSizeComboBox.SelectedItem);
            }

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string rtfile = System.IO.Path.Combine(Environment.CurrentDirectory, $"{viewModel.SelectedNote.Id}.rtf");
            viewModel.SelectedNote.FileLocation = rtfile;
            DataBaseHelpers.Update(viewModel.SelectedNote);

            FileStream fileStream = new FileStream(rtfile, FileMode.Create);
            var content = new TextRange(contentRichTextBox.Document.ContentStart, contentRichTextBox.Document.ContentEnd);
            content.Save(fileStream, DataFormats.Rtf);
            fileStream.Close();
        }
    }
}