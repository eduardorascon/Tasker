   M S B A M L   `   `   `  ����A  =Tasker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Tasker.ClassesTasker.Classes  5      +    %Tasker.View.QA.TestCases.StepListView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      0classesclr-namespace:Tasker.Classes     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      $	��300��5        � DataContext  5
      �� 5       �� Path$ StepListViewModel�6      Locator �� Source# [  6%   .��5      �  � RowDefinitions 5      �� 5      $��20 �6   �� 5      $��28 �6   �� 5      .+�5      � $��0��6	   ��5      �� 5        AccentColor �� Color# �  6   6>   .+�5      �� $��Center=�6   $��Center=�6-   $��	Step List��6H   $��13(�6Y   $��SemiBold"�6g   5      "   Tasker.Classes.FilterControl  5$      $��1��6   $��22��6'      Header$ Search item here��63      FilterTextBindingPath$ Description��5&         TargetControl 5%      ��  �� ElementName$ StepList��	   ListTextBindingPath	 5(      "   Tasker.Classes.FilterAdapter  
   Title$
 Description��6      PropertyBound$ Description��60   5*      �� 5/      -   5         ��Name$ StepList��5/      $��2��50      ���	#4F4F4D4D
������   RedGlowItemContainer1#��[ 53      ��54      �� *
StepList+ �� SelectedItem 55      �� *SelectedStepItem+ J� InputBindings 57      d� 58       d� MouseAction$ LeftDoubleClicke�6    �� Command 59      �� *NewSubTaskCommand+��5<      �� 5=      .R�5>      � ���	#20A9A9A9$
��Left=�5@      $	��Top=�5A      $	��300��5B      .+�5C      �  5F   	   �� 5G   
   $	��15* �6   �� 5H   
   $	��25* �6    � ColumnDefinitions 5K   	   �� 5L   
   $��30 �6   �� 5M   
   $	��270 �6   .+�5R   	   �� $��0��5S   
   $��0��5T   
   $��13(�5U   
   $
��Auto��5V   
   $��Center=�5W   
   $
��Bold"�5X   
     ItemText#���  5Y   
   $
��Wrap=�5Z   
   ��5[   
   �� *	RowNumber+F� 5]   	    F�Name$ PathTestPlan��6   $��1��6$   $��0��61   $��Center=�6A   $��Center=�5^      ����p��  @�-G@QP�  @�-G@Q.�M   �xQ@Q.�M   ��Q@QP�   �fW@Q��   �fW@Q��   ��T@Q��e   ��T@    ��4@   9�T@     62@   �oT@    �0@   �0T@	  ��,@   y�S@	  ���)@   �gS@Q^в    ��R@     &@   9;R@Q]9�    �~Q@Q]9�   @�G@QV3�   @�G@QV3�   @�OI@    ��0@���_��J@     *2@  @��K@    �D4@  @2L@Q��e  @�L@Q��  @�L@Q��  @�-G@	 �� Stretch$ Uniform=�5`      $��20��6   $��20��6*   $��5,0,5,5q�66    J� RenderTransformOrigin$ 0.5,0.5&�5a      
���  ��6,   �� 5c   	   $��1��5d      $��0��5e      $	��270��5f   4   $��13(�5g      $
��Auto��5h      $��1,0,0,0q�5i      $
��Bold"�5j      $��CharacterEllipsis=�5k   4   #���  5l      ��5c      �� *Description+5l      5o   	   5p      5q      5s      5w      5x      