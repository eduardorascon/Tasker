   M S B A M L   `   `   `  ����A  =Tasker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Tasker.View.QA.TestCasesTasker.View.QA.TestCases  5      )clr-namespace:Tasker.ViewTasker.View  5      /clr-namespace:Tasker.ClassesTasker.Classes  5	      3    -Tasker.View.QA.TestCases.RunExecutionMainView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      5QA&clr-namespace:Tasker.View.QA.TestCases     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      G CPixelLab.Wpf, Version=6.0.2.0, Culture=neutral, PublicKeyToken=nullAwpf7http://schemas.thinkpixellab.com/2011/xaml/presentation  5      $viewclr-namespace:Tasker.View  5      *classesclr-namespace:Tasker.Classes  5	        � DataContext  5      �� 5       �� Path$ RunExecutionMainViewModel�6      Locator �� Source# [  6-   .��5      � .+�5      �  � ColumnDefinitions 5      �� 5      $	��210 �6   �� 5      $	��190 �6   �� 5      $	��300 �6    � RowDefinitions 5      �� 5      $��20 �6   �� 5      $	��120 �6   �� 5      $��28 �6   �� 5      $	��68* �6   .+�5$      � $��0��6
   $��3��6   ��5%      �� 5&        AccentColor �� Color# �  6   6?   .+�5(      �� $��Center=�6   $��Center=�6.   $��Run Execution Panel��6I   $��13(�6d   $��SemiBold"�6r   5)      � 5/      $��1��6
   $��0��6   $��3��6'    52      �� 53      $��70 �6   �� 54      $��* �6   �� 55      $��80 �6   �� 56      $��* �6    58      �� 59      $��40 �6   �� 5:      $��* �6   �� 5;      $��30 �6$   .+�5C      �� $��
Execution:��6   $��0��5D      $
��Left=�5E      $	��Top=�5F      $��5q�5G      $
��Bold"�5H      �� 5J      $��Input:��6   $��1��5K   
   $
��Left=�5L   
   $	��Top=�5M   
   $��5q�5N   
   $
��Bold"�5O   
   �� 5Q      $��Description:��6   $��0��5R   
   $��2��5S   
   $
��Left=�5T   
   $	��Top=�5U   
   $��5q�5V   
   $
��Bold"�5W   
   �� 5Y      $��Output:��6   $��2��5Z   
   $��1��5[   
   $
��Left=�5\   
   $	��Top=�5]   
   $��5q�5^   
   $
��Bold"�5_   
   �� 5a      $��Porcent:��6   $��0��5b   
   $��2��5c   
   $
��Left=�5d   
   $	��Top=�5e   
   $��5q�5f   
   $
��Bold"�5g   
   �� 5i      $��Status:��6   $��2��5j   
   $��2��5k   
   $
��Left=�5l   
   $	��Top=�5m   
   $��5q�5n   
   $
��Bold"�5o   
   �� 5s      $��0��5t      $��1��5u      $��5q�5v      $	��Top=�5w      ��5s      �� * ReceivedExecutionDetail.ExecId+5w      �� 5y      $��1��5z   
   $��1��5{   
   $��5q�5|       �� 
IsReadOnly .  �� TextWrapping$
 Wrap=�5~   
   $��Center=�5   
   $��45��6%   ��5y      �� *ReceivedStepItem.Input+5   %   �� 5�      $��0��5�   
   $��3��5�   
   $
��Wrap=�5�      $��5q�5�   
   $	��Top=�5�   
   ��5�      �� *ReceivedStepItem.Description+5�   
   �� 5�      $��3��5�   
   $��1��5�   
    . $
 Wrap=�5�   
   $��5q�5�   
   $��Center=�5�   
   $��45��6%   ��5�      �� *!ReceivedStepItem.ExpectedResult+5�   %   �� 5�      $��1��5�   
   $��2��5�   
    . $
 Wrap=�5�   
   $��5,0,5,5q�5�      $��Center=�5�   
   $��30��6%   ��5�      �� *RunExecItemDisplay.Porcent+ �� StringFormat$ {0:F2}%��5�   %   �� 5�      $��3��5�   
   $��2��5�   
    . $��5,0,5,5q�5�      $��Center=�5�   
   $��30��6%   ��5�      �� *!ReceivedStepItem.ApprovedReject+5�   %   5�      "   Tasker.Classes.ToolBarButton  5�      $��0��5�      $��2��6   $
��Left=�6#   $��Center=�5�        appbar_list1	   MetroImageSource#	 [ 5�      
 � ToolTip$
 Show / hide steps list��5�      ��5�   >   �� *ShowTestCaseListCommand+5�      9   3Tasker.View.QA.TestCases.RunExecutionControlBarView  5�      $��2��6$   $��Right=�5�      $��2��5�      $��0,0,5,0q�6   � 5�      $��3��6
   $��3��6   $	��Top=�6,    5�      �� 5�      $
��auto �6   �� 5�      $
��auto �6   �� 5�      $��* �6   .+�5�      �� -   5         ��Name$ RevealContainer��5�      $��0��5�      $��0,0,0,0q�5�      $��2��5�      ._�5�        PixelLab.Wpf.Reveal  -   5          Name$ TaskDetailsReveal��5�         Duration$	 150Q�5�   	      HorizontalReveal$ FromLeftToRight=�5�   	      
IsExpanded 5�   	   �� *ShowTestCaseList+._�5�      �  5�   	   �� 5�   
   $��* �6   6'   �� 5�   
   $��* �6   6'    5�   	   �� 5�   
   $��* �6   6%   .+�5�   	   ;   5Tasker.View.QA.TestCases.RunExecutionTestCaseListView  $��0��6*   $��0��5�      $��5q�5�      7   1Tasker.View.QA.TestCases.RunExecutionStepListView  5�   	   $��1��6&   $��0��5�      $��5q�5�      5�   	   5�      5�      �� 5�      $��2��5�      $��5q�5�      $��Stretch=�5�      $��Stretch=�5�      .��5�   	   �� -   5         ��Name$ imagebox��5�   
   $��5q�5�      ��5�   
   �� *ImageSourceProp+5�      5�      �� 5�      -   5         ��Name$ BlackScreenBlock��5�       J� Opacity$ .7Q�5�      
���   �$��0��5�      $��3��6   $��0��5      $��5��6   y�5     �� *ShowBlackBlockScreen+ �� Mode$ TwoWay=�5       5     -   5        $ FaultExecutionReveal��5     $	 150Q�5	     $ FromRightToLeft=�5
      5     �� *ExpandFaultExecutionPanel+._�5     A   ;Tasker.View.QA.TestCases.RunExecutionFaultExceptionMainView  $	��500��6+   5     5     5     