using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.TextFormatting;
using CommunityToolkit.Mvvm.Input;
using MapCreatorModels.Models.Assets;

namespace MapCreator.Windows
{
    public class TextureDrawerViewModel : INotifyPropertyChanged
    {
        #region Property
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private Texture _topLeft;
        public Texture TopLeft
        {
            get { return _topLeft; }
            set
            {
                _topLeft = value;
                NotifyPropertyChanged(nameof(TopLeft));
            }
        }
        private Texture _topCenter;
        public Texture TopCenter
        {
            get { return _topCenter; }
            set
            {
                _topCenter = value;
                NotifyPropertyChanged(nameof(TopCenter));
            }
        }
        private Texture _topRight;
        public Texture TopRight
        {
            get { return _topRight; }
            set
            {
                _topRight = value;
                NotifyPropertyChanged(nameof(TopRight));
            }
        }
        private Texture _centerLeft;
        public Texture CenterLeft
        {
            get { return _centerLeft; }
            set
            {
                _centerLeft = value;
                NotifyPropertyChanged(nameof(CenterLeft));
            }
        }
        private Texture _center;
        public Texture Center
        {
            get { return _center; }
            set
            {
                _center = value;
                NotifyPropertyChanged(nameof(Center));
            }
        }
        private Texture _centerRight;
        public Texture CenterRight
        {
            get { return _centerRight; }
            set
            {
                _centerRight = value;
                NotifyPropertyChanged(nameof(CenterRight));
            }
        }
        private Texture _bottomLeft;
        public Texture BottomLeft
        {
            get { return _bottomLeft; }
            set
            {
                _bottomLeft = value;
                NotifyPropertyChanged(nameof(BottomLeft));
            }
        }
        private Texture _bottomCenter;
        public Texture BottomCenter
        {
            get { return _bottomCenter; }
            set
            {
                _bottomCenter = value;
                NotifyPropertyChanged(nameof(BottomCenter));
            }
        }
        private Texture _bottomRight;
        public Texture BottomRight
        {
            get { return _bottomRight; }
            set
            {
                _bottomRight = value;
                NotifyPropertyChanged(nameof(BottomRight));
            }
        }

        private GameColor _selectedGameColor;
        public GameColor SelectedGameColor
        {
            get { return _selectedGameColor; }
            set
            {
                _selectedGameColor = value;
                NotifyPropertyChanged(nameof(SelectedGameColor));
            }
        }

        private bool _duplicateDisplay;
        public bool DuplicateDisplay
        {
            get { return _duplicateDisplay; }
            set
            {
                _duplicateDisplay = value;
                ToggleMirorDisplay(value);
                NotifyPropertyChanged(nameof(DuplicateDisplay));
            }
        }
        #endregion


        public ICommand SaveTextureCommand { get; }
        private void SaveTexture()
        {
            AppSingleton.Instance.TextureCache.UpdateWritableBitmap(_textureToUpdate,_tempTexture);
            _textureToUpdate.UpdateTexture(_tempTexture);
        }

        public ICommand DeleteCacheCommand { get; }
        private void DeleteCache()
        {
            AppSingleton.Instance.TextureCache.DeleteTextureFromCache(_tempTexture);
        }

        private Texture _tempTexture;
        private Texture _textureToUpdate;


        public TextureDrawerViewModel(Texture texture)
        {
            _textureToUpdate = texture;
            _tempTexture = (Texture)texture.Clone();
            SaveTextureCommand = new RelayCommand(SaveTexture);
            DeleteCacheCommand = new RelayCommand(DeleteCache);
            Center = _tempTexture;
        }

        public void ToggleMirorDisplay(bool toggle)
        {
            Texture texture = toggle ? Center : null;
            TopLeft = texture;
            TopCenter = texture;
            TopRight = texture;
            CenterLeft = texture;
            CenterRight = texture;
            BottomLeft = texture;
            BottomRight = texture;
            BottomCenter = texture;
        }
    }
}
