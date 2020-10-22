import numpy as np
import pandas as  pd
import matplotlib.pyplot as plt

def VA1(x):
    if(x<=1 or x>=4):
        return 0
    elif (x>=2 and x<=3):
        return 1
    elif (x>1 and  x<2):
        return x - 1
    elif (x>3 and  x<4):
        return 4 - x
  

x_a1 = np.linspace(1,4,100)
y_a1 = np.vectorize(VA1)(x_a1)
print(y_a1)
plt.plot(x_a1, y_a1)




