   M S B A M L   `   `   `  ����A  =Tasker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Tasker.ClassesTasker.Classes  5      0    *Tasker.View.QA.TestCases.ExecutionListView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      .classesclr-namespace:Tasker.Classes    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        � DataContext  5
      �� 5       �� Path$ ExecutionListViewModel�6      Locator �� Source# [  6*   .��5      �  � RowDefinitions 5      �� 5      $��20 �6   �� 5      $��28 �6   �� 5      .+�5      � $��0��6	   ��5      �� 5        AccentColor �� Color# �  6   6>   .+�5      �� $��Center=�6   $��Center=�6-   $��Execution List��6H   $��13(�6^   $��SemiBold"�6l   5      "   Tasker.Classes.FilterControl  5"      $��1��6   $��22��6'      Header$ Search item here��63      FilterTextBindingPath$ Description��5$         TargetControl 5#      ��  �� ElementName$ StepList��	   ListTextBindingPath	 5&      "   Tasker.Classes.FilterAdapter  
   Title$
 Description��6      PropertyBound$ Description��60   5(      � 5+      $��2��6	   .+�5,      �� $
��Auto=�6   .��5-      �� -   5        $��LstItems��5.      ���	#4F4F4D4D
������   RedGlowItemContainer1#��[ 51      ��52      �� *ExecutionList+ �� SelectedItem 53      �� *SelectedExecutionItem+��55      �� 56      .R�58   	   � ���	#20A9A9A9$��60��6%    � ColumnDefinitions 59   
   �� 5:      $��40 �6   �� 5;      $	��280 �6    5>   
   �� 5?      �� 5@      .+�5C   
   �� $��0��5D   
   $��0��6   $	��Top=�5E   
   $��Center=�6"   $��0q�5F   
   $��12(�5G   
   $
��Auto��5H   
   $
��Bold"�5I   
     ItemText#���  5J   
   $
��Wrap=�5L   
   ��5K   
   �� *ExecId+5L   
   �� 5N   
   $��1��5O   
   $��0��5P   
   $��0q�5Q   
   $��Stretch=�5R   
   #���  5S   
   $
��Wrap=�5U   
   ��5T   
   �� *Description+5U   
   5Y   
   5Z   	   5\      5]      5^      5a      5b      