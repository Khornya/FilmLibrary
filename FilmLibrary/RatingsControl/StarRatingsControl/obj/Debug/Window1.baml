   M S B A M L   `   `   `  ����M  IStarRatingsControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:StarRatingsControlStarRatingsControl  5           StarRatingsControl.Window1   5   	   \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      8x,http://schemas.microsoft.com/winfx/2006/xaml    5      0local clr-namespace:StarRatingsControl    5        � Title$  Window1��5      $	��300��6   $	��300��6"   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   ��  �� Orientation$ Vertical=�6   $��0��6,   .+�5      '   !StarRatingsControl.RatingsControl  -   5          Name$ ratings1��5   #      Value$	 4.2~�5         NumberOfStars$ 5��5      $��5q�5      $
��Left=�5        5      -   5        $ ratings2��5   #   $	 4.2~�5      $ 5��5      $��5q�5      $
��Left=�5      5      �� 5   
   $ 
Horizontal=�6   $��1��6.   .+�5      �� $��Num of stars:��6   $	��150��6,   �� 5      -   5         ��Name$ txtNumStars��5      $	��120��6+   5       �� 5"   
   $ 
Horizontal=�6   $��2��6.   .+�5#      �� $��Value:��6   $	��150��6%   �� 5$      -   5        $ txtValue��5$      $	��120��6(   5%      �� 5'   
   -   5        $��3��5'      $��Go��6   $��5q�6+   $	��120��66   $��Center=�5(      5)      5+      5,      