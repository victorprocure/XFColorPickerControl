using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace XFColorPickerControl
{
    public sealed class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Color _pickColor;
        public Color PickColor { get => _pickColor; set => SetProperty(ref _pickColor, value, onAfterSet: () => { NotifyPropertyChanged(nameof(ColorHex)); NotifyPropertyChanged(nameof(FaceColor)); }); }
        public string ColorHex => PickColor.ToHex();
        public Color FaceColor => PickColor.Luminosity < 0.5 ? Color.White : Color.Black;

        private void SetProperty<T>(ref T store, T value, Action onAfterSet = default, [CallerMemberName] string propertyName = "")
        {
            var isSame = EqualityComparer<T>.Default.Equals(store, value);
            if (isSame)
            {
                return;
            }

            store = value;
            NotifyPropertyChanged(propertyName);
            onAfterSet?.Invoke();
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return;
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
