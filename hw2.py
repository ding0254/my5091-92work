import numpy as np
import pandas as pd
def option(S0,K,vol,T,N,r,q,optiontype):  #construct a function for option price: underlying asset price;strike price;volitality;time;number of stage;interest rate;divident rate;option type 
    opt=np.ones(N+1)                      #construct a list to store the option price on different stage
    t=T/N
    u=np.exp(vol*np.sqrt(t))              #upper rate
    d=np.exp(-vol*np.sqrt(t))             #lower rate
    p=(np.exp((r-q)*t)-d)/(u-d)           #probability measure
    if(optiontype=='europeancalloption'):   #european call option
        for i in range(N+1):
            opt[i]=max(np.round(S0*d**(i)*u**(N-i),decimals = 5)-K,0)   #the option value at exercising time from different underlying asset price
        for n in range(N,-1,-1):
            for i in range(n):
                opt[i]=np.round(np.exp(-r*t)*(p*opt[i]+(1-p)*opt[i+1]),decimals=5)  #calculate the previous option using the latter option value
        opt00=opt[0]  #the option value at start time
        opt10=(opt[0]*np.exp(r*t)-(1-p)*opt[1])/p  #option price stage(1,0) has been replaced by (0,0),so we need to caculate it again
        opt11=opt[1]    
        opt22=opt[2]
        opt21=(opt11*np.exp(r*t)-(1-p)*opt22)/p   #caculate the option price at stage(2,1)
        opt20=(opt10*np.exp(r*t)-(1-p)*opt21)/p   #caculate the option price at stage(2,0)
    elif(optiontype=='americancalloption'):   # american call option
        for i in range(N+1):
            opt[i]=max(np.round(S0*d**(i)*u**(N-i),decimals = 5)-K,0)   #the option value at exercising time from different underlying asset price
        for n in range(N,2,-1):    #only caculate to the second stage 
            for i in range(n):
                opt[i]=np.round(max(np.exp(-r*t)*(p*opt[i]+(1-p)*opt[i+1]),S0*d**(i)*u**(n-1-i)-K),decimals=5) ##calculate the previous option using the latter option value
        opt20=opt[0]
        opt21=opt[1]
        opt22=opt[2]
        opt10=np.round(max(np.exp(-r*t)*(p*opt20+(1-p)*opt21),S0*d**(0)*u**(1)-K),decimals=5) #using the definition of american option to caculate value
        opt11=np.round(max(np.exp(-r*t)*(p*opt21+(1-p)*opt22),S0*d**(1)*u**(0)-K),decimals=5)
        opt00=np.round(max(np.exp(-r*t)*(p*opt10+(1-p)*opt11),S0*d**(0)*u**(0)-K),decimals=5)
    elif(optiontype=='europeanputoption'):       #european put option
        for i in range(N+1):
            opt[i]=max(K-np.round(S0*d**(i)*u**(N-i),decimals = 5),0)  #the option value at exercising time from different underlying asset price
        for n in range(N,-1,-1):
            for i in range(n):
                opt[i]=np.round(np.exp(-r*t)*(p*opt[i]+(1-p)*opt[i+1]),decimals=5)  
        opt00=opt[0]
        opt10=(opt[0]*np.exp(r*t)-(1-p)*opt[1])/p
        opt11=opt[1]
        opt22=opt[2]
        opt21=(opt11*np.exp(r*t)-(1-p)*opt22)/p
        opt20=(opt10*np.exp(r*t)-(1-p)*opt21)/p
    elif(optiontype=='americanputoption'):  #american put option
        for i in range(N+1):
            opt[i]=max(K-np.round(S0*d**(i)*u**(N-i),decimals=5),0)
        for n in range(N,2,-1):  #caculate to the second stage 
            for i in range(n):
                opt[i]=np.round(max(np.exp(-r*t)*(p*opt[i]+(1-p)*opt[i+1]),K-S0*d**(i)*u**(n-1-i)),decimals=5)  
        opt20=opt[0]
        opt21=opt[1]
        opt22=opt[2]
        opt10=np.round(max(np.exp(-r*t)*(p*opt20+(1-p)*opt21),K-S0*d**(0)*u**(1)),decimals=5)
        opt11=np.round(max(np.exp(-r*t)*(p*opt21+(1-p)*opt22),K-S0*d**(1)*u**(0)),decimals=5)
        opt00=np.round(max(np.exp(-r*t)*(p*opt10+(1-p)*opt11),K-S0*d**(0)*u**(0)),decimals=5)    
    else:
        return "wrong optiontype"    #input wrong optiontype
    return [opt00,opt10,opt11,opt20,opt21,opt22]   #return a list of option value at different stages


def output(S0,K,vol,T,N,r,q,optiontype,delta_vol,delta_r):  #define a function for option and greek
    t=T/N
    u=np.exp(vol*np.sqrt(t))    #upper rate
    d=np.exp(-vol*np.sqrt(t))   #lower rate
    p=(np.exp((r-q)*t)-d)/(u-d)   #measure probability
    options=option(S0,K,vol,T,N,r,q,optiontype)  #using the fuction option
    delta=(options[2]-options[1])/(d*S0-u*S0)     #delta=c(1,1)-c(1,0)/(s(1,1)-s(1,0))
    gamma=((options[5]-options[4])/(d*d*S0-u*d*S0)-(options[4]-options[3])/(u*d*S0-u*u*S0))/(0.5*(d*d*S0-u*u*S0)) #gamma 
    vega=(option(S0,K,vol+delta_vol,T,N,r,q,optiontype)[0]-option(S0,K,vol-delta_vol,T,N,r,q,optiontype)[0])/(2*delta_vol) #vega
    theta=(options[4]-options[0])/(2*t)    #theta
    rho=(option(S0,K,vol,T,N,r+delta_r,q,optiontype)[0]-option(S0,K,vol,T,N,r-delta_r,q,optiontype)[0])/(2*delta_r)  #rho
    data=[['price :' , np.round(options[0],decimals=4)],['delta :', delta],['gamma :' ,gamma],['vega :' ,vega],['theta :' ,theta],['rho :',  rho]]
    print(pd.DataFrame(data))   
        
    

output(100,100,0.2,1,500,0.06,0.01,"europeancalloption",0.0001,0.0006)  #sample


        
        
        
        
        
        
        
        
        
        
        