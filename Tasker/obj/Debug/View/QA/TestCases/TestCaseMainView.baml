   M S B A M L   `   `   `  ����A  =Tasker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Tasker.View.QA.TestCasesTasker.View.QA.TestCases  5      G CPixelLab.Wpf, Version=6.0.2.0, Culture=neutral, PublicKeyToken=nullA0clr-namespace:PixelLab.Wpf;assembly=PixelLab.WpfPixelLab.Wpf 5      /    )Tasker.View.QA.TestCases.TestCaseMainView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      5QA&clr-namespace:Tasker.View.QA.TestCases     5      @wpf0clr-namespace:PixelLab.Wpf;assembly=PixelLab.Wpf     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        � DataContext  5      �� 5       �� Path$ TestCaseMainViewModel�6      Locator �� Source# [  6)   .��5      �  � RowDefinitions 5      �� 5      $��30 �6   �� 5      $
��auto �6   $	��600��6*   �� 5      $��* �6    � ColumnDefinitions 5      �� 5      $	��350��6   $
��350* �6%   $	��350��62   �� 5      $��11 �6   �� 5      $	��330 �6   .+�5       � -   5         �Name$ 
ControlBar��5    	   $��0��5!      $	��Top=�5"      $��5q�5#      .+�5$      5   /Tasker.View.QA.TestCases.TestCaseControlBarView  5%      � 5'      -   5        $ ControlBarStepList��5'   	   $��0��5(      $��2��5)      $	��Top=�5*      $��5q�5+      .+�5,      >   8Tasker.View.QA.TestCases.TestCaseStepsListControlBarView  5-      �� 54      -   5         ��Name$ RevealContainer��54      $��1��55      $��2��6   ���	#3FD2D2D2y�56      �� *ShowHeaderTestCase+._�58        PixelLab.Wpf.Reveal  -   5          Name$ TaskDetailsReveal��58         Duration$	 150Q�59      $	��Top=�5:      $
��Left=�5;      	   
IsExpanded	 5<      �� *ShowNewTestCaseHeader+
 �� Mode$
 TwoWay=�._�5=      +   %Tasker.View.QA.TestCases.TestCaseView  -   5          Name$ 	oTaskView��5=      5>      5?      �� 5B      -   5        $ RevealContainer2��5B      $��1��5C      $��0��6   $��3��5D      ���	#3FD2D2D2y�5E      �� *ShowAddNewStep+$
 TwoWay=�._�5G        -   5        $ TestCaseDetailsReveal��5G      $	 150Q�5H      $	��Top=�5I      $
��Left=�5J      	 5K      �� *ShowAddNewStepHeader+$
 TwoWay=�._�5L      /   )Tasker.View.QA.TestCases.AddStepsMainView  -   5          Name$ oTestCaseDetailView��5L   '   $	��600��6   6'   5M      5N      � 5U   
   -	   5        $ Detalle��5U      $��2��5W      $��2��5X      $	��Top=�5Y      $��0,0,10.111,0q�5Z      y�5V      �� *ShowListTestCaseDetail+$
 TwoWay=�.+�5[      ,   &Tasker.View.QA.TestCases.TestCasesView  5\      �� 5_   
   -
   5        $ RevealListStepBorder��5_      $��2��5a      $��2��5b      ���	#3FD2D2D2y�5`      �� *ShowListTestCaseDetail+$
 TwoWay=�._�5e      +   %Tasker.View.QA.TestCases.StepListView  -   5          Name$ 	oStepView��5e   +   $	��330��6   6+   5f      �� 5l      -   5         ��Name$ BlackScreenBlock��5l      $��2��5m      $��4��6    J� Opacity$ .7Q�5n      
���   �y�5o      �� *ShowBlackBlockScreen+$
 TwoWay=��� 5r   
   -   5        $ BlackScreenBlock2��5r      $��1��5s      $��3��6   $ .7Q�5t      
���   �y�5u      �� *ShowBlackBlockScreen+$
 TwoWay=��� 5x      -   5        $ BlackScreenBlockButtonBar��5x      $��0��5y      $��3��5z      $ .7Q�5{      
���   �y�5|      �� * ShowBlackBlockScreenControlBar+$
 TwoWay=��� 5      -   5        $' !BlackScreenBlockButtonBarStepList��5      $��0��5�      $��3��5�      $ .7Q�5�      
���   �y�5�      �� *(&ShowBlackBlockScreenControlBarStepList+$
 TwoWay=�5�      5�      