   M S B A M L   `   `   `  ����A  =Tasker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:GalaSoft.MvvmLightGalaSoft.MvvmLight  5          Tasker.View.SprintView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      6	mvvmLight clr-namespace:GalaSoft.MvvmLight     5      $	��300��5        � DataContext  5	      �� *SprintViewModel+   Locator �� Source# [   +� FocusedElement 5      ��  �� ElementName$ 	TituloTxt��.��5      � $��1q�6    � ColumnDefinitions 5   
   �� 5      $	��80* �6   �� 5      $
��112* �6   �� 5       � RowDefinitions 5   
   �� 5      $��1* �6   6*   �� 5      $��1* �6   6*   �� 5      $��1* �6   6*   �� 5      $��1* �6   6*   �� 5      $��4* �6   6*   .+�5   
   �� $��0��6   $��0��6    .��6-   Team:64   �� 5   
   $��1��6   $��0��6    ��6-   �� *SelectedSprint.TeamId+6Y   �� 5    
   $��0��6   $��1��6    .��6-   
Project:67   �� 5!   
   $��1��6   $��1��6    ��6-   �� *SelectedSprint.ProjectId+6\   �� 5"   
   $��0��6   $��2��6    .��6-   Sprint Number:6=   �� 5#   
   $��2��6   $��25��6   $��1��6+    �� 	MaxLength$ 30��6h   ��6;   �� *SelectedSprint.SprintName+6y   �� 5$   
   $��0��6   $��3��6#    d� 	IsChecked 60   �� *#!SelectedSprint.OverWriteJQLFilter+.��6i   Use JQL Filter:6z   �� 5%   
   $��4��6   $	��0,2q�6    �� TextWrapping$
 Wrap=�6,   $��0��6@   $��2��6P   $	 100��5&   K   ��5%   d   �� *SelectedSprint.JqlFilter+{�5&      �� *#!SelectedSprint.OverWriteJQLFilter+6]   5,      5.      