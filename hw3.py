import numpy as np
import scipy.optimize 
import pandas as pd
from scipy.stats import norm
d1 = lambda S,K,r,vol,T: (np.log(S/K) + (r + (vol ** 2) / 2) * T) / (vol * np.sqrt(T)) 
d2 = lambda S,K,r,vol,T: (np.log(S/K) + (r - (vol ** 2) / 2) * T) / (vol * np.sqrt(T)) 
#define function to compute callprice
callprice = lambda S,K,r,vol,T: S * norm.cdf(d1(S,K,r,vol,T)) -K * np.exp(-r * T) * norm.cdf(d2(S,K,r,vol,T))
putprice=lambda S,K,r,vol,T: K * np.exp(-r * T) * norm.cdf(-d2(S,K,r,vol,T))-S * norm.cdf(-d1(S,K,r,vol,T))
#define function to compute vega
vega= lambda S,K,r,vol,T:S*norm.pdf(d1(S,K,r,vol,T))*T**0.5
#define function to use bisection
def bisection(s,k,T,r,optprice,size):
    vol1=np.zeros(size)   #lower boundary 
    vol2=10*np.ones(size) #upper boundary
    iter=0          #num of iteration
    F = lambda x: (callprice(s, k, r, x, T) - optprice)  #x is a variable for vol
    while sum((np.abs(vol1-vol2)<1e-7))<size:    #define the accuracy
        vol3=0.5*(vol1+vol2)              #the middle value of the lower and upper boundary
        a=F(vol3)
        lowbools=(a>0)            
        upbools=(a<0)
        vol1=vol1*lowbools+vol2*upbools
        vol2=np.maximum(vol1,vol3)   #the new value of the upper boundary 
        vol1=np.minimum(vol1,vol3)   #the new value of the lower boundary
        iter=iter+1
    return [vol2,iter]  

def newton(s,k,T,r,optprice,size):   #the Newton method
    F = lambda x: (callprice(s, k, r, x, T) - optprice)
    df= lambda x: vega(s,k,r,x,T)        #df is a function of vol to compute vega
    deltax=np.ones(size)               #deltax is the adjustment to the previous guess 
    iter=0
    x0=0.5*np.ones(size)       #assume the start value of vol             
    while sum(np.abs(deltax)<1e-7) <size and iter<10:   #to make sure the accuracy and not to go into infinite loop
        x1=x0+(0-F(x0))/df(x0)     #use the Newton method formula 
        deltax=x1-x0
        x0=x1
        iter=iter+1
    return [x1,iter]

def output(S,K,T,r,optprice,size):    #output function :optprice means option price and size means the number of price                
     bisectionvol = bisection(S, K, T, r, optprice,size) #the value of bisction method
     newtonvol=newton(S, K, T, r, optprice,size)       #the value of Newton method
     data=[['bisction:',bisectionvol[0]],['iter:', bisectionvol[1]],['newton:', newtonvol[0]],['iter:', newtonvol[1]]] 
     print(pd.DataFrame(data))
     
#test
size=10000     
S=50*np.ones(size)      #construct the vector of S
K=50*np.ones(size)      #construct the vector of striking price
r=0.05*np.ones(size)    #construct the interest rate
T=np.ones(size)         #construct the tenor
optprice=np.linspace(10,11,size)   #construct the call option price
output(S,K,T,r,optprice,size)   

