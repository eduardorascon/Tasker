   M S B A M L   `   `   `  ����A  =Tasker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Tasker.ClassesTasker.Classes  5      @    :Tasker.View.QA.TestCases.ExecutionMainViewTestCaseListView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      0classesclr-namespace:Tasker.Classes     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        � DataContext  5	      �� 5
       �� Path$, &ExecutionMainViewTestCaseListViewModel�6      Locator �� Source# [  6:   .��5      �  � RowDefinitions 5      �� 5      $��20 �6   �� 5      $��28 �6   �� 5      .+�5      "   Tasker.Classes.FilterControl  $��1q�6   $��22��6&      Header$ Search��62   $��1��5         FilterTextBindingPath$ Description��5         TargetControl 5      ��  �� ElementName$ ListTestCaseItems��   ListTextBindingPath 5      "   Tasker.Classes.FilterAdapter  	   Title$	 Description��6   
   PropertyBound$
 Description��60   5!      � 5'      $��0��6	   ��5(      �� 5)        AccentColor �� Color# �  6   6>   .+�5+      �� $��Center=�6   $��Center=�6-   $��Item for Execution��6H   $��13(�6b   $��SemiBold"�6p   5,      �� 50      $��Visible=�6   $��2��67   .��51      � -   5         �Name$ Items��51   
    55      �� 56      $
��Auto �6   �� 57      $��* �6    � ColumnDefinitions 5:      �� 5;      .+�5B      �� -   5        $��ListTestCaseItems��5C      $��1��5B      ���	#4F4F4D4D
������   RedGlowItemContainer1#��[ 5F      ��5G      �� *ExecutionDetailListItem+ �� SelectedItem 5H      �� *ExecutionDetailItem+��5J      �� 5K      .R�5M   	   � ���	#20A9A9A9$
��Left=�5N   	   $��Center=�5O   	   $	��320��5P   	    5Q   
   �� 5R      $��40 �6   �� 5S       5V   
   �� 5W      �� 5X      .+�5[   
   �� $��0��6   $��0��6$   $	��Top=�5\      $��Center=�5]      $��0,0,7,0q�5^      $��12(�5_      $
��Auto��5`      $
��Bold"�5a        ItemText#���  5b      $
��Wrap=�5d      ��5c      �� *
TestCaseId+5d      F� 5g   
    F�Name$ PathTestPlan��6   $��1��6%   $��0��62   $��Center=�6B   $��Center=�5h      ����pG�� X�~=
@�G�� ����(�� fv���`�>` @���o	e�"@�o8S]R�@�v���v�ӾE�o8N�����O}fv����z���Q��zX�~=
@���     Ѧ��     �Z�R    Ѻɚ    �y�    Qy�6�f
 �?Qy���;��?Qy��{��g�?Q�ɚ�{��g�?Q�ɚ0��",@Q�ɚ���=�@Ѻɚeo��2�>�ǼZLa#ǼZ�3��ǼZ�I��� ��]- ������  ��ߴ� ���#ܿ@����CH'\�@ΕK����3��    ��&7"s�	�@!���� p
�Q��� �`"�@Q��� 0��",@Q��� �{��g�?Q�� �{��g�?Q�� ��;��?Q�� 6�f
 �?	 �� Stretch$ Uniform=�5j      $��20��6   $��20��6(   $��5q�64    J� RenderTransformOrigin$ 0.5,0.5&�5k      
��� ���6+   �� 5n   
   $��1��5o   	   $��0��5p   	   $��0,0,7,0q�5q   	   $��Stretch=�5r   	   #���  5s   	   $
��Wrap=�5u   	   ��5t   	   �� *Description+5u   	   �� 5}   
   $��1��6   $��1��5~      $��28,0,0,5q�5      $��Stretch=�5�      $��10(�5�      #���  5�      $
��Wrap=�5�      ��5�      �� *
Objetive+5�      5�   
    �� Triggers 5�   	   �� 5�   
    �� Value$ False��68    �� Binding 6   �� $ IsActive�.a�5�      ��  �� 
TargetName$ PathTestPlan��6    �� Property � �� �� Value
 �����6<   5�      5�   	   5�      5�      5�      5�      5�      