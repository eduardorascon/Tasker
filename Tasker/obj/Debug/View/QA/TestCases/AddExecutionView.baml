   M S B A M L   `   `   `  ����A  =Tasker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Tasker.View.QA.TestCasesTasker.View.QA.TestCases  5      /clr-namespace:Tasker.ClassesTasker.Classes  5      /    )Tasker.View.QA.TestCases.AddExecutionView   5      -QA&clr-namespace:Tasker.View.QA.TestCases  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      .classesclr-namespace:Tasker.Classes    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        � DataContext  5      �� 5   
    �� Path$ AddExecutionViewModel�6      Locator �� Source# [  6/   .��5      �  � ColumnDefinitions 5   
   �� 5      $��45 �6   �� 5      $	��39* �6    � RowDefinitions 5   
   �� 5      $��20 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��1* �6   .+�5!   
   � $��0��6   $��2��6   ��5"      �� 5#        AccentColor �� Color# �  6"   6J   .+�5%      �� $��Center=�6   $��Center=�66   $��Add Execution��6Q   $��13(�6f   $
��Bold"�6t   5&      9   3Tasker.View.QA.TestCases.AddExecutionControlBarView  5,   
   $��1��6(   $��0��66   $��2��6F   $
��Left=�6Z   $��0,0,5,0q�6u   �� 52   
   $
��Name��6   $��2��53      $
��Left=�54      $��5q�55      $��Center=�56      �� 58   
   $��Status��6   $��3��59      $
��Left=�5:      $��5q�5;      $��Center=�5<      �� 5A   
   $��5q�5C      $��2��5D      $��1��5E       �� TextWrapping$
 Wrap=�5F      $	��350��5G      $	��300��5H      $
��Left=�5I      ��5B      �� *!ExecutionHeaderItem.Description+ �� Mode$ TwoWay=�5I      �� 5L   
   {�.  $��3��5O      $��5q�5P      $��1��5Q      $
 Wrap=�5R      $	��350��5S      $	��300��5T      $
��Left=�5U      ��5M      �� *ExecutionHeaderItem.Status+$ TwoWay=�5U      � 5[   
   $��4��5\      $��5q�5]      $��2��5^       5a      �� 5b      $	��250 �6#   �� 5c      $	��210 �6#   �� 5d      $��30 �6#   �� 5e      $	��210 �6#   .+�5j      ;   5Tasker.View.QA.TestCases.AddExecutionTestPlanListView  $��0��6/   $��0,0,5,0q�6?   ;   5Tasker.View.QA.TestCases.AddExecutionTestCaseListView  5o      $��1��6.   $��0,0,5,0q�6>   3   -Tasker.View.QA.TestCases.AddExecutionListView  5t      $��3��6&   $��0,0,5,0q�66   � 5w      $��2��5x      $��Center=�5y       5{      �� 5|      $��30 �6$   �� 5}      $��30 �6$   .+�5�      "   Tasker.Classes.ToolBarButton  $��0��5�   (   $��Center=�5�   (   $��Center=�5�   (     toolbar_addarrow   MetroImageSource# [ 5�   (   	 � ToolTip$	 Run Selected Execution��5�   (   {�5�   (   �� *EnableAddTestCase+��5�   (   �� *AddTestCaseCommand+  5�      $��1��5�   (   $��Center=�5�   (   $��Center=�5�   (     toolbar_removearrow# [ 5�   (   $	 Run Selected Execution��5�   (   {�5�   (   �� *EnableRemoveTestCase+��5�   (   �� *RemoveTestCaseCommand+5�      5�      5�      5�      