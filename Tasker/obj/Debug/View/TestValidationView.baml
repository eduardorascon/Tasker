   M S B A M L   `   `   `  ����A  =Tasker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Tasker.ClassesTasker.Classes  5      $    Tasker.View.TestValidationView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    67   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     6<   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      (ignorehttp://www.ignore.com     6L   0Classesclr-namespace:Tasker.Classes     5        +� FocusedElement  6L   ��  �� ElementName$ 	TituloTxt�� � DataContext 6�   �� *Task+   Locator �� Source# [  .��5      � .+�5      ��  �� Orientation$ Vertical=�6   .+�5      �� -   5         ��Name$ 	TituloTxt��5      $��2q�6    $��80��6+   $��25��66    +� IsFocusScope . +   %Tasker.Classes.FocusManagerExtensions   	IsFocused$
 trueX�5
      ��5      ��  �� Path$ SelectedTask.Title�	 �� Mode$	 TwoWay=�
 �� UpdateSourceTrigger$
 PropertyChanged=�" �� ValidatesOnNotifyDataErrors . 5   2   �� 5      $��2q�6   $��80��6    . $��25��5       5      �� *	IsFocused+��5      �� $ SelectedTask.StringHour�$	 TwoWay=� �� StringFormat$
 {00}��$
 PropertyChanged=�5      �� 5      $��2q�6   $��80��6   $��25��5      ��5      �� $ SelectedTask.StringMinute�$	 TwoWay=�$
 {00}��$
 PropertyChanged=�5      �� 5      $��2q�6   $��80��6   $��25��5"      ��5      �� $ SelectedTask.StringSecond�$	 TwoWay=�$
 {00}��$
 PropertyChanged=�5"      �� 5$      ��6   �� *
NewCommand+.��6,   New61   �� 5%      ��6   �� *ValidateCommand+.��61   
Validate6;   �� 5'      $��1��6   
���  ��$��0,5,0,0q�5*      ��5(      �� *ValidationErrorsString+5*      5+      5,      5-      