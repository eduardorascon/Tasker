   M S B A M L   `   `   `  ����A  =Tasker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Tasker.ControlsTasker.Controls  5      /clr-namespace:Tasker.ClassesTasker.Classes  6=   7 clr-namespace:GalaSoft.MvvmLightGalaSoft.MvvmLight  5          Tasker.View.TaskView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      2Controlsclr-namespace:Tasker.Controls     5      0Classesclr-namespace:Tasker.Classes     6=   6	MvvmLight clr-namespace:GalaSoft.MvvmLight     5      $	��350��6D   $	��130��5        � DataContext  5	      �� *Task+   Locator �� Source# [   +� FocusedElement 5      ��  �� ElementName$ 	TituloTxt�� � 	Resources 5        validationTemplate � ToolTip �� Path ��Name �� 
TargetType	 �� Property
 �� Value  ItemText��%:  &	       '��     �� 5   
   .R�5      ]� .+�5      �� $��5,0,5,0q�6   
���  ��$��16(�6>   $��Center=�6L   $��*��6g    6p   �� $ errorAdorner��$8 2AdornedElement.(Validation.Errors)[0].ErrorContent��� 5      $ errorAdorner��6,   5      5      �� 5   
   " ��6   .�5      �� 	 � ��

 ���� 6+   �� 5      	 � ��$
 0q�60   �� 5      	 �@��AcceptsReturn
 .  6.   �� 5      	 � ��$
 SemiBold"�6+   �� 5      	 � ��#
 �  6+   �� 5      	 �@=�ErrorTemplate#
 �  69   5      5       .��5#      �  � RowDefinitions 5$   
   �� 5%      $��54 �6   �� 5&      $��30 �6   �� 5'      $��30 �6   .+�5*   
   � $��1��6   $��Center=�6!   $��0,6,0,2q�6<   $��22��6M   .+�5+      �� #���  6   .��6<   	Category:6G   �� 5,      -   5        $��ComboBox��5,      $	��100��5-       �� DisplayMemberPath$ 
CategoryId��5.      $ �� IsSynchronizedWithCurrentItem$
 TrueY�5/      -  'System.Windows.Input.KeyboardNavigation   TabIndex$ 1��51       �� SelectedValuePath$ 
CategoryId��58      ��50      �� *
Categories+ �� SelectedItem 52      �� $ SelectedCategory� �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=� �� SelectedValue 55      �� $ SelectedTask.Category�$ TwoWay=�$ PropertyChanged=�58      �� 59      $��20��6   $��20��5:      $	��6,0q�5;      ��5<      �� *%#SelectedCategory.CategoryColorBrush+5=      �� 5>      #���  6   .��6<   Time:6C   �� 5?      $��30��6   $��Center=�5@       �� AcceptsReturn .  $��SemiBold"�5B      $ 2��5C      ��5D      �� *SelectedTask.StringHour+$ TwoWay=� �� StringFormat$
 {00}��$ PropertyChanged=��� 5E      #���  6   .��6<   :6?   �� 5F      $��30��6   $��Center=�5G   "    .  $��SemiBold"�5I   "   $ 3��5J   "   ��5K   "   �� $ SelectedTask.StringMinute�$ TwoWay=�$
 {00}��$ PropertyChanged=��� 5O      #���  6   .��6<   :6?   �� 5P      $��30��6   $��Center=�5Q   "    .  $��SemiBold"�5S   "   $ 4��5T   "   ��5U   "   �� $ SelectedTask.StringSecond�$ TwoWay=�$
 {00}��$ PropertyChanged=�5Z      � 5\   
   $��Center=�6   $��54��6/   .+�5^      �� -   5         ��Name$ 	TituloTxt��5^   "   $	��348��6   $��50��5_   "   $	��300��5`   "   $��1,4,1,1q�5a   "   $��Bottom=�5b   "    +� IsFocusScope . |�. $ 0��5f   "    �� 	MaxLength$	 140��5g   "    �� MaxLines$ 3��5h   "    �� TextWrapping$
 Wrap=�5o   "   +   %Tasker.Classes.FocusManagerExtensions   	IsFocused 5e   "   �� *	IsFocused+��5i   "   �� $ SelectedTask.Title�$ TwoWay=�$ PropertyChanged=� �� NotifyOnValidationError .  �� ValidatesOnDataErrors .  �� ValidatesOnExceptions . 5o   "   5p      � 5r   
   $��2��6   $��0,6,0,2q�6!   $��22��62   .+�5s      �� #���  6   $��Center=�6<   $��Center=�6Y   .��5t      	Status:6   �� 5u      -   5         ��Name$ EstadoTarea��5u      $
 TrueY�6,   $ 5��5v   
   $��80��6*   $��Center=�65   $��Center=�6R   ��5w   
   �� *StatusTaskCol+ 5x   
   �� *SelectedTask.Status+$ TwoWay=�$ PropertyChanged=�5z      5|      5}      