   M S B A M L   `   `   `  ����A  =Tasker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Tasker.ClassesTasker.Classes  5      ;    5Tasker.View.QA.TestCases.RunExecutionTestCaseListView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      0classesclr-namespace:Tasker.Classes     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      $	��300��5   #     � DataContext  5
      �� 5   
    �� Path$' !RunExecutionTestCaseListViewModel�6      Locator �� Source# [  6;   .��5      � .+�5   
   �  � RowDefinitions 5      �� 5      $��20 �6    �� 5      $��28 �6    �� 5      .+�5      � $��0��6   ��5      �� 5         AccentColor �� Color# �  6&   6N   .+�5"      �� $��Center=�6   $��Center=�6:   $��Test Case List��6U   $��13(�6k   $��SemiBold"�6y   5#      "   Tasker.Classes.FilterControl  5)      $��1q�6$   $��22��6/   $��1��6;      Header$ Search item here��5*          FilterTextBindingPath$ Description��5,          TargetControl 5+       ��  �� ElementName$ LstItems��	   ListTextBindingPath	 5.      "   Tasker.Classes.FilterAdapter  
   Title$
 Description��6,      PropertyBound$ Description��6@     5/      $
 	Objective��6,   $ Objetive��6>   51      � 56      -   5         �Name$ Items��56      $��2��6"    5:      �� 5;      $
��Auto �6$   �� 5<      $��* �6$   .+�5C      �� -   5        $��ListTestCaseItems��5D      $��1��5C      ���	#4F4F4D4D
������   RedGlowItemContainer1#��[ 5G      ��5H      �� *ExecutionDetailList+ �� SelectedItem 5I      �� *SelectedExecutionDetailItem+��5K      �� 5L      .R�5N      � ���	#20A9A9A9 � ColumnDefinitions 5O   "   �� 5P   &   $��40 �67   �� 5Q   &   $	��230 �67    5T   "   �� 5U   &   �� 5V   &   .+�5Y   "   �� $��0��6,   $��0��6<   $	��Top=�5Z   (   $��Center=�5[   (   $��0,0,5,0q�5\   (   $��12(�5]   (   $
��Auto��5^   (   $
��Bold"�5_   (     ItemText#���  5`   (   $
��Wrap=�5b   (   ��5a   (   �� *
TestCaseId+5b   (   F� 5d   "    F�Name$ PathTestPlan��6'   $��1��6=   $��0��6J   $��Center=�6Z   $��Center=�5e   #   ����pG�� X�~=
@�G�� ����(�� fv���`�>` @���o	e�"@�o8S]R�@�v���v�ӾE�o8N�����O}fv����z���Q��zX�~=
@���     Ѧ��     �Z�R    Ѻɚ    �y�    Qy�6�f
 �?Qy���;��?Qy��{��g�?Q�ɚ�{��g�?Q�ɚ0��",@Q�ɚ���=�@Ѻɚeo��2�>�ǼZLa#ǼZ�3��ǼZ�I��� ��]- ������  ��ߴ� ���#ܿ@����CH'\�@ΕK����3��    ��&7"s�	�@!���� p
�Q��� �`"�@Q��� 0��",@Q��� �{��g�?Q�� �{��g�?Q�� ��;��?Q�� 6�f
 �?	 �� Stretch$ Uniform=�5g   #   $��20��65   $��20��6@   $��5q�6L    J� RenderTransformOrigin$ 0.5,0.5&�5h   #   
��� ���6C   �� 5j   "   $��1��5k   !   $��0��5l   !   $��0,0,7,0q�5m   !   $��Stretch=�5n   !   #���  5o   !   $
��Wrap=�5q   !   ��5p   !   �� *Description+5q   !   5s       �� Triggers 5t      �� 5u   "    �� Value$ False��6P    �� Binding 6.   �� $ IsActive�.a�5v   &   ��  �� 
TargetName$ PathTestPlan��6-    �� Property � �� �� Value
 �����6W   5w   #   5y      5{      5�      5�      5�      5�      