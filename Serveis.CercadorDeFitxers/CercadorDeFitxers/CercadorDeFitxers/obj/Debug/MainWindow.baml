   M S B A M L   `   `   `  ����L  HCercadorDeFitxers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:CercadorDeFitxersCercadorDeFitxers  5   	   "    CercadorDeFitxers.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:CercadorDeFitxers     5   	   V RMaterialDesignThemes.Wpf, Version=2.5.1.1327, Culture=neutral, PublicKeyToken=null@material1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   $	��600��5      $	��400��6)     � Title$  Cercador��5	   	   $	��400��6   $	��600��6'   .��5
      ]� .+�5   
   '  !MaterialDesignThemes.Wpf.Snackbar  -   5          Name$ snackbar��5      $��Bottom=�6.   $��Stretch=�6F      Message. 5      .  (MaterialDesignThemes.Wpf.SnackbarMessage  $��Falten paràmetres��6'   5      �� 5   
   $
��Left=�6   
�������$	��250��6B   .+�5      �� -   5         ��Name$ entryDirectory��5      $��Stretch=�6.   $��Stretch=�5      )  #MaterialDesignThemes.Wpf.HintAssist   
IsFloating .    Hint$ 	Directori��5      $0��*C:\Users\ruben\Dropbox\M06 - AccesDades\F2��6;   .  (MaterialDesignThemes.Wpf.TextFieldAssist   HasFilledTextField . $��
10,10,10,0q�5      $��20(�6*    �� BaselineOffset$ 0Q�68   �� 5      -   5        $ entryNomFitxer��5      $��1��6.   $��Stretch=�6;   $��Stretch=�5       . $ 
Nom fitxer��5      $	��*.*��6<    . $��
10,10,10,0q�5      $��20(�6*   $ 0Q�68   �� 5      -   5        $ entryContingutFitxer��5      $��2��64   $��Stretch=�6A   $��Stretch=�5       . $ Contingut del fitxer��5      $��a��6F    . $��
10,10,10,0q�5      $��20(�6*   $ 0Q�68   �� 5      $��3��6   $��0,10,0,0q�6&    �� Orientation$ 
Horizontal=�68   $��Center=�6Q   .+�5      �� -   5        	 ��Name$	 btnStart��5      $��0,0,10,0q�6+   $��0q�5       +  %MaterialDesignThemes.Wpf.ShadowAssist
   ShadowDepth$
 Depth0=�6-   .��6X   Cercar6`   �� 5!      -   5        $	 btnStop��5!      $��0q�5"      $
 Depth0=�6-   .��6X   Parar6_   5#      �� 5$      -   5         ��Name$ lblFinished��5$      $��Center=�6)   $
��Bold"�6F   
��� � �$��10q�5%      $��20(�6    $��Finished��6.   
�������$y�	Collapsed=�5&      5'      ]� 5(   
   $��	10,0,10,0q�6   .+�5)      �� $��	Resultats��6   $��25(�6)   $��	10,10,0,0q�67   $	��Top=�5*      �� 5+      -   5         ��Name$ lvResultats��5+      $��0,10,0,0q�6,   $��Bottom=�6>   $
��Auto=�5,      $��Stretch=�5-      $��Stretch=�5.      $��Stretch=�63   5/      50      52      53      