   M S B A M L   `   `   `  ����A  =Tasker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:GalaSoft.MvvmLightGalaSoft.MvvmLight  5      +    %Tasker.View.QA.TestCases.TestCaseView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      6	mvvmLight clr-namespace:GalaSoft.MvvmLight     5        � DataContext  5
      �� *TestCaseViewModel+   Locator �� Source# [   +� FocusedElement 5      ��  �� ElementName$ 	TituloTxt��.��5      � $��1q�6    � ColumnDefinitions 5   
   �� 5      $��80 �6   �� 5      $
��112* �6   �� 5       � RowDefinitions 5   
   �� 5      $��0 �6   6)   �� 5      $��0 �6   6)   �� 5      $��20 �6   6*   �� 5      $��40 �6   6*   �� 5      $��1* �6   6*   �� 5      $��20 �6   6*   �� 5      $��40 �6   6*   �� 5      $��20 �6   6*   �� 5      $��40 �6   6*   �� 5       $��20 �6   6*   �� 5!      $��40 �6   6*   �� 5"      $��1* �6   6*   �� 5#      $��2 �6   6)   .+�5'   
   �� $��Center=�6   $��0��6+   $y�	Collapsed=�6;   $��0��6R   .��6_   Test Case Id:6n   �� 5(   
   $��Center=�6   $��0��6+   $y�	Collapsed=�6;   $��1��6R   .��6_   Created Date:6n   �� 5)   
   $��Center=�6   $��0��6+   $��2��6;   .��6H   Description:6V   �� 5*   
   $��Center=�6   $��0��6+   $��4��6;   .��6H   	Duration:6S   �� 5+   
   $��Center=�6   $��0��6+   $��5��6;   .��6H   
Objective:6T   �� 5,   
   $��Center=�6   $��0��6+   $��7��6;   .��6H   
Test Data:6T   �� 5-   
   $��Center=�6   $��0��6+   $��9��6;   .��6H   Pre-Condition:6X   �� 5.   
   $��Center=�6   $��0��6+   $��11��6;   .��6I   Test Plan Id:6X   �� 56   
   $��2��6   $	��0,2q�6    �� TextWrapping$
 Wrap=�6,   $��1��6@   $��2��6P   $	��270��6�   ��6b   �� *" ReceivedTestCaseItem.Description+ �� Mode$ TwoWay=�6�   �� 57   
   $��4��6   $��40��6   $��1��6*   $
��Left=�6j   ��6:   �� *ReceivedTestCaseItem.Duration+6j   �� 58   
   $��5��6   $	��0,2q�6   $
 Wrap=�6,   $��1��6@   $��2��6P   $	��270��6�   ��6a   �� *ReceivedTestCaseItem.Objetive+$ TwoWay=�6�   �� 59   
   $��7��6   $	��0,2q�6   $
 Wrap=�6,   $��1��6@   $��2��6P   $	��270��6�   ��6a   �� *ReceivedTestCaseItem.TestData+$ TwoWay=�6�   �� 5:   
   $��9��6   $	��0,2q�6    $
 Wrap=�6-   $��1��6A   $��2��6Q   $	��270��6�   ��6b   �� *#!ReceivedTestCaseItem.PreCondition+$ TwoWay=�6�   �� 5;   
   $��1��6   $��11��6"   $��25��60    �� 
IsReadOnly . {�.  	 �� 	MaxLength$	 50��6�   ��6<   �� *SelectedTestPlanId+$ TwoWay=�6�   5>      5@      