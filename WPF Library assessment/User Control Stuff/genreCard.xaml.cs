using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPF_Library_assessment.User_Control_Stuff
{
    public partial class genreCard : UserControl
    {
        public genreCard()
        {
            InitializeComponent();
            DataContext = this;
            
        }


        public static readonly DependencyProperty TitleGenreProperty = DependencyProperty.Register(
            "TitleGenre", typeof(string), typeof(genreCard), new PropertyMetadata(default(string)));

        public string TitleGenre
        {
            get { return (string)GetValue(TitleGenreProperty); }
            set { SetValue(TitleGenreProperty, value); }
        }

        public static readonly DependencyProperty ImageGenreProperty = DependencyProperty.Register(
            "ImageGenre", typeof(ImageSource), typeof(genreCard), new PropertyMetadata(default(string)));

        public ImageSource ImageGenre
        {
            get { return (ImageSource)GetValue(ImageGenreProperty); }
            set { SetValue(ImageGenreProperty, value); }
        }
    }
}
