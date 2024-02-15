﻿using MapCreatorModels.Models.Assets;
using System.Windows;

namespace MapCreator.Windows
{
    /// <summary>
    /// Interaction logic for TextureDrawer.xaml
    /// </summary>
    public partial class TextureDrawerWindow : Window
    {
        bool toggle;
        TextureDrawerViewModel textureDrawerViewModel;
        public TextureDrawerWindow(Texture texture)
        {
            InitializeComponent();

            textureDrawerViewModel = new TextureDrawerViewModel(texture);
            this.DataContext = textureDrawerViewModel;
        }

        private void ThisWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            textureDrawerViewModel.DeleteCacheCommand.Execute(null);
        }
    }
}
