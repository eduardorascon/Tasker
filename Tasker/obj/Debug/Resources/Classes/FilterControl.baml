   M S B A M L   `   `   `  ����A  =Tasker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Tasker.ClassesTasker.Classes  5   \   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   ClearButtonStyle  �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� CornerRadius J� SnapsToDevicePixels F�Name �� Triggers	 P� Property
 P� Value �� 
TargetName  
ListBorder �� Color  FocusBackground �� EndPoint �� 
StartPoint � Color � Offset  FilterTextBoxStyle ��Name"    Tasker.Classes.FilterControl � ColumnDefinitions �� Stretch  ItemText J� RenderTransformOrigin �� Angle �� ScaleX �� ScaleY ��Name �� AutoWordSelection �� RelativeSource �� AncestorType �� Path �� Mode �� UpdateSourceTrigger ��Name     Header! J� IsHitTestVisible" �� DisplayMemberPath# �� SelectedValuePath$    ListTextBindingPath% �� SelectedValue& ��Name' P� 
SourceName�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      8d2http://schemas.microsoft.com/expression/blend/2008  6L   Bmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      (classesclr-namespace:Tasker.Classes  6\   %�  &	        &	 �    &	     &	 �    '   �    7  7   �� 5      "  ��6%   .�5   
   ��  � ��
 ���� 6(   �� 5	   
    � ��
 ���� 6'   �� 5
   
    � �� 5      �� 5      " ��6"   .R�5      �� $ border��6   $ 2��6-   #��z��6>   $��1q�6h   #��z��5      $��16��6I   $��16��6T    . ._�5      � $��8��6   $��8��6)   $��Center=�64   $��Center=�6Q   .+�5      F� $ path1��6#   ���#ADADAD����        � z  z 	$��2��6T   F� 5      $ path2��6#   ���#ADADAD���� z     �     z 	$��2��6T   5      5       5      P� 5      	 �@J�IsMouseOver
 . . �5      �� $ path1��67    � ��
 �  ��6J   �� 5      $ path2��67    � ��
 �  ��6J   5      5%      5'      5(      �� 5*      $ 	#FF7F9DB9��6)   �� 5,      $ 0.5,0&�62   $ 	0.5,1.667&�6C   .��5-   
   � $ 	#FF073553��6   $ 0Q�6)   � 5.   
   $ 	#FFA7D5F4��6   $ 0.796Q�6)   � 5/   
   $ 	#FF5A88A8��6   $ 1Q�6)   50      �� 52      "  ��6'   .�53   
   ��  � ��$ 2q�6$   �� 55   
    �@J�	AllowDrop . 6&   �� 56   
    � �� 57      �� 58      " ��6"   .R�59      �� $ Bd��6    . ._�5:      �� $ PART_ContentHost��6'   # z 6A   5;       5<      P� 5=      	 � {�
 .  . �5>      �� $ Bd��6;   # � ��6K    � ���� 5?      # � ��6;    � ��5@      5B      5D      �� 5E   
    � ��$ 0,0,0,0q�6,   5F      �� 5H      "    6   .�5J   
   ��  �@  FilterOnEnter .  6*   �� 5K   
    � �� 5L      �� 5M      "   6"   .R�5N      �� $ border��6   
������ ._�5O      � $��1q�6    5P      �� 5Q   "   $
��Auto �63   �� 5R   "   $��* �63   �� 5S   "   $
��Auto �63   �� 5T   "   $
��Auto �63   �� 5U   "   $
��Auto �63   .+�5W      �� $��4��6%   $��Stretch=�69   $��Stretch=�6U   � 5X      $��backgroundGrid��6#   $��16��69   $��18��6D   .+�5Y      (� .��5Z   "   � .+�5\   &   F� ����� 5E��ĝ��c&�)�4��P۸h������7�Ӡ��Ԙ� ��Zc ���B�R �dfV�B ��� p� h�| �|VS{l 2U0*�s@@��� ����T��ڔ�u�l$���&S@@�א�"T�{ѰD��f����ȵ�h�X��4�<�o�(�YtsV��]��* 5E����� 8ja�̚� |{�\Q|W� �T���� �tMܝ� �HT�_%��J(9�ӈ�@�;��i��K( ]�|��o.�#K� �X���� 8ja$ Uniform=�5^   )   #���  6;   $��16��6]   $��16��6h   $��0,0,0,0q�6t   $ 0.5,0.5&�6�   z�5_   *   V� 5`   .   ~�5b   6   �� $ 0Q�6F   �� 5c   6   $ 1Q�6E   $ 1Q�6P   5e   /   5g   '   5h   #   5i      5j      �� 5k      $ PART_FilterBox��6R   8��   . $��1��5l   &   $��0,1,0,1q�66   $��Center=�6G   ��5m   &   ��  �� "   $ 
FilterText�$ TwoWay=�$ PropertyChanged=��� 5n      $ PART_Header��6(   #��z  6=   $��Center=�6]   $
��Left=�5o   (   $��2,0,0,0q�6C   $��1��6T   ! .  ���#ADADAD6}   �� 5p      $��	Filter by��6(   $��Center=�69   $
��Left=�5q   (   $��2,0,2,0q�6C   $��2��6T   ! .  ���#ADADAD6}   �� 5r      $��PART_HeaderComboBox��6'   $	��100��6B   $��3��6R   $��0,0,0,0q�6b   $" Title��5s   '   $# PropertyBound��5t   '   #��z$ 5u   '   $	��200��6[   $��0q�5v   '   % 5w   '   ��  �� "   $ FilterTextBindingPath�$ TwoWay=�$ PropertyChanged=��� 5y      $& PART_ClearButton��6%   $��4��6?   $��0,0,4,0q�6O   8�� $y�	Collapsed=�5z   O   5{      5|       5}      P� 5~      $' PART_FilterBox��6D   	 �@J�	IsFocused
 . . �5      �� $ border��6M    � �� �	#50ADADAD5�      P� 5�      $' PART_ClearButton��6D   	 �@J�	IsFocused
 . . �5�      �� $ border��6O    � ��
 ���� 5�      P� 5�      	 �@J�	IsFocused
 . . �5�      �� $ border��6M    � �� �	#50ADADAD5�      5�      5�      5�      5�      