   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35N;clr-namespace:System.Windows;assembly=PresentationFrameworkSystem.Windows 5      A =Tasker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Tasker.ControlsTasker.Controls 5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   DefaultErrorTemplate  �Name � ColumnDefinitions ��Name �� CornerRadius � ToolTip �� ElementName �� Path �� 
TargetType �� Property	 �� Value  textBoxInError
 �� Triggers P� Property P� Value �� Self �� RelativeSource�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      1System&clr-namespace:System;assembly=mscorlib  5      Cvsm;clr-namespace:System.Windows;assembly=PresentationFramework  5      *Controlsclr-namespace:Tasker.Controls  5      %  &	        '�� n    7   'G� �    7   &	     �� 5      .R�5      � $  	container��6    5   
   �� 5	      $��* �6   .+�5   
   �� $��customAdorner��5      $��0��5   $   $��Center=�5      5      �� 5   
   $ ValidationErrorElement��6   ���	#FFDB000C$��1q�5      $ 1��5       5      �� $ customAdorner��$8 2AdornedElement.(Validation.Errors)[0].ErrorContent�._�5      � 
������ $��Right=�5   	   $��12��5   	   $��	1,-4,-4,0q�5   	   $	��Top=�5   	   $��12��5   	   .+�5      F� ;����@B     р�[     � z �� ��� �� �J]� z ��j 	���	#FFDC000C$��1,3,0,0q�5   
   F� 5      +����        р�     � z ��[ � z  z 	���#ffffff$��1,3,0,0q�5!   
   5"      5#      �� 5%   
   $��1��6   
���  ��$��Right=�5'      $��Center=�5(      $��15��5)      $��15��5*      $��0,0,5,0q�5+      $ 10��5,       5-      �� $ customAdorner��$8 2AdornedElement.(Validation.Errors)[0].ErrorContent�._�5.      �� $��!��6   $��center=�5/      $��center=�50      $
��Bold"�51      
�������53      54      55      56      �� 58      " ��6   .�59   
   ��  �@=�ErrorTemplate8	   5:      5;      �� 5<      " G�6   .�5=   
   ��  �@=�ErrorTemplate8	   5>      5?      �� 5A      " ��6#   
 5B   
   P� 5C       �@=�HasError . . �5D      ��  �@�ToolTip	 5E      �� # Z $) #(Validation.Errors)[0].ErrorContent�5G      5I      5J      