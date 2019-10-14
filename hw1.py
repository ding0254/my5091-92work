import numpy as np
import math
from math import exp
from scipy.stats import norm 
def phi(x):   #construct cdf function
    cdf=norm.cdf(x, loc = 0, scale = 1)
    return cdf

def PHI(x):    #construct pdf function
    pdf=norm.pdf(x, loc = 0, scale = 1)
    return pdf


def solve_d(S,K,r,sigma,T):   ##construct parameter d1&d2
    d1=(math.log(S/K)+(r+sigma**2/2)*T)/(sigma*T**0.5)
    d2=d1-sigma*T**0.5
    return [d1,d2]

def solve_call(S,K,r,sigma,T):  ##CALL OPTION
    D=solve_d(S,K,r,sigma,T)
    call=S*phi(D[0])-K*exp(-r*T)*phi(D[1])
    return call

def solve_put(S,K,r,sigma,T):  ##PUT OPTION
    D=solve_d(S,K,r,sigma,T)
    put=K*exp(-r*T)*phi(-D[1])-S*phi(-D[0])
    return put

def solve_calldelta(S,K,r,sigma,T):  ## delta of call option
    D=solve_d(S,K,r,sigma,T)
    delta=phi(D[0])
    return delta

def solve_putdelta(S,K,r,sigma,T):   ##delta of put option
    D=solve_d(S,K,r,sigma,T)
    delta=phi(D[0])-1
    return delta

def solve_callgamma(S,K,r,sigma,T):   ##gamma of option
    D=solve_d(S,K,r,sigma,T)
    gamma=PHI(D[0])/(S*sigma*T**0.5)
    return gamma

def solve_callvega(S,K,r,sigma,T):   ##vega of option
    D=solve_d(S,K,r,sigma,T)
    vega=S*PHI(D[0])*T**0.5
    return vega

def solve_calltheta(S,K,r,sigma,T):    ##theta of call option
    D=solve_d(S,K,r,sigma,T)
    theta=-S*PHI(D[0])*sigma/(2*T**0.5)-r*K*exp(-r*T)*phi(D[1])
    return theta

def solve_puttheta(S,K,r,sigma,T):     ##theta of put option
    D=solve_d(S,K,r,sigma,T)
    theta=-S*PHI(D[0])*sigma/(2*T**0.5)+r*K*exp(-r*T)*phi(-D[1])
    return theta

def solve_callrho(S,K,r,sigma,T):        ##rho of call option
    D=solve_d(S,K,r,sigma,T)
    rho=K*T*exp(-r*T)*phi(D[1])
    return rho

def solve_putrho(S,K,r,sigma,T):       ##rho of put option
    D=solve_d(S,K,r,sigma,T)
    rho=-K*T*exp(-r*T)*phi(-D[1])
    return rho

#test
solve_callrho(50,50,0.05,0.5,1)
solve_calltheta(50,50,0.05,0.5,1)
solve_callvega(50,50,0.05,0.5,1)
solve_call(50,50,0.05,0.5,1)
solve_callgamma(50,50,0.05,0.5,1)
solve_calldelta(50,50,0.05,0.5,1)

solve_put(50,50,0.05,0.5,1)
solve_putdelta(50,50,0.05,0.5,1)
solve_putrho(50,50,0.05,0.5,1)
solve_puttheta(50,50,0.05,0.5,1)




