   M S B A M L   `   `   `  ����A  =Tasker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Tasker.ClassesTasker.Classes  5      3    -Tasker.View.QA.TestCases.AddExecutionListView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      0classesclr-namespace:Tasker.Classes     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        � DataContext  5
      �� 5   
    �� Path$ AddExecutionListViewModel�6      Locator �� Source# [  63   .��5      �  � RowDefinitions 5      �� 5      $��20 �6   �� 5      $��28 �6   �� 5      .+�5      "   Tasker.Classes.FilterControl  $��1q�6   $��22��6&      Header$ Search��62   $��1��5         FilterTextBindingPath$ Description��5         TargetControl 5      ��  �� ElementName$ ListTestCaseItems��   ListTextBindingPath 5       "   Tasker.Classes.FilterAdapter  	   Title$	 Description��6   
   PropertyBound$
 Description��60   5"      � 5(   
   $��0��6   ��5)      �� 5*        AccentColor �� Color# �  6"   6J   .+�5,      �� $��Center=�6   $��Center=�66   $��Item for Execution��6Q   $��13(�6k   $��SemiBold"�6y   5-      �� 51   
   $��Visible=�6   $��2��6=   .��52   
   � -   5         �Name$ Items��52       56      �� 57      $
��Auto �6   �� 58      $��* �6   .+�5?      �� -   5        $��ListTestCaseItems��5@      $��1��5?      ���	#4F4F4D4D
������   RedGlowItemContainer1#��[ 5C      ��5D      �� *ExecutionDetailListItem+ �� SelectedItem 5E      �� *ExecutionDetailItem+��5G      �� 5H      .R�5J      � ���	#20A9A9A9$
��Left=�5K      $��Center=�5L      $	��175��5M       � ColumnDefinitions 5N   	   �� 5O   
   $��40 �6   �� 5P   
    5S   	   �� 5T   
   �� 5U   
   .+�5X   	   �� $��0��6   $��0��6#   $	��Top=�5Y      $��Center=�5Z      $��0,0,7,0q�5[      $��12(�5\      $
��Auto��5]      $
��Bold"�5^        ItemText#���  5_      $
��Wrap=�5a      ��5`      �� *
TestCaseId+5a      F� 5d   	    F�Name$ PathTestPlan��6   $��1��6$   $��0��61   $��Center=�6A   $��Center=�5e      ����pG�� X�~=
@�G�� ����(�� fv���`�>` @���o	e�"@�o8S]R�@�v���v�ӾE�o8N�����O}fv����z���Q��zX�~=
@���     Ѧ��     �Z�R    Ѻɚ    �y�    Qy�6�f
 �?Qy���;��?Qy��{��g�?Q�ɚ�{��g�?Q�ɚ0��",@Q�ɚ���=�@Ѻɚeo��2�>�ǼZLa#ǼZ�3��ǼZ�I��� ��]- ������  ��ߴ� ���#ܿ@����CH'\�@ΕK����3��    ��&7"s�	�@!���� p
�Q��� �`"�@Q��� 0��",@Q��� �{��g�?Q�� �{��g�?Q�� ��;��?Q�� 6�f
 �?	 �� Stretch$ Uniform=�5g      $��20��6   $��20��6(   $��5q�64    J� RenderTransformOrigin$ 0.5,0.5&�5h      
��� ���6+   �� 5k   	   $��1��5l   	   $��0��5m   !   $��0,0,7,0q�5n   !   $��Stretch=�5o   !   #���  5p   !   $
��Wrap=�5r   !   ��5q   !   �� *Description+5r   !   �� 5z   	   $��1��6   $��1��5{      $��28,0,0,5q�5|      $��Stretch=�5}      $��10(�5~      #���  5      $
��Wrap=�5�      ��5�      �� *
Objetive+5�      5�   	    �� Triggers 5�      �� 5�   	    �� Value$ False��67    �� Binding 6   �� $ IsActive�.a�5�   
   ��  �� 
TargetName$ PathTestPlan��6    �� Property � �� �� Value
 �����6;   5�   
   5�      5�      5�      5�      5�      5�      