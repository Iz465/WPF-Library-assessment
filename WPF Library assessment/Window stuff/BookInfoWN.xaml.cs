using System;
using System.Windows;

namespace WPF_Library_assessment.Window_stuff
{
    public partial class BookInfoWN : Window
    {
        public BookInfoWN()
        {
            InitializeComponent();
            DataContext = this;

            // Example usage
            textInfo("Author", "TextAuthor");
        }

        public void textInfo(string name, string bindName)
        {
            var property = DependencyProperty.Register(
                name,
                typeof(string),
                typeof(BookInfoWN));

            // Register attached property
            RegisterAttachedProperty(property, bindName);
        }

        private void RegisterAttachedProperty(DependencyProperty property, string bindName)
        {
            // Define attached property get and set methods
            var attachedProperty = DependencyProperty.RegisterAttached(
                bindName,
                typeof(string),
                typeof(BookInfoWN),
                new PropertyMetadata(null));

            // Register the attached property with the owner class
            RegisterAttached(bindName, property, attachedProperty);
        }

        private void RegisterAttached(string bindName, DependencyProperty property, DependencyProperty attachedProperty)
        {
            // Add get and set methods for the attached property
            var getMethod = typeof(BookInfoWN).GetMethod($"Get{bindName}");
            var setMethod = typeof(BookInfoWN).GetMethod($"Set{bindName}");

            if (getMethod == null || setMethod == null)
            {
                throw new InvalidOperationException("Failed to register attached property.");
            }

            // Define a lambda expression for the get and set methods
            var getLambda = CreateGetLambda(property);
            var setLambda = CreateSetLambda(property);

            // Attach the get and set methods to the attached property
            var propertyMetadata = new PropertyMetadata(
                null,
                (d, e) => setLambda(d, (string)e.NewValue));

            attachedProperty.OverrideMetadata(typeof(BookInfoWN), propertyMetadata);
        }

        private Func<DependencyObject, string> CreateGetLambda(DependencyProperty property)
        {
            return obj => (string)obj.GetValue(property);
        }

        private Action<DependencyObject, string> CreateSetLambda(DependencyProperty property)
        {
            return (obj, value) => obj.SetValue(property, value);
        }
    }
}
