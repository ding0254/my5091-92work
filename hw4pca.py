#pca
import numpy as np 
import pandas as pd
import pyodbc 
def getreturn(ticker):   #get the return from table HistPrices in sql
    conn = pyodbc.connect('Driver={SQL Server};' 'Server=LAPTOP-9LI79KT8;' 'Database=MFM_Financial;' 'Trusted_Connection=yes;')
    result=pd.read_sql("select ClosePrice from [MFM_Financial].[FinData].[HistPrices],[MFM_Financial].[FinData].[Instrument]\
                  where [Instrument].ID=[HistPrices].InstID\
                  and [instrument].StockTicker='" + ticker + "'",con=conn) #get closeprice using the InstID to connect table instrument and HistPrices
    returns = pd.DataFrame()
    for i in result: 
        returns[i] = np.log(result[i][1:].values/result[i][:-1].values)  #caculate the return 
    return(returns)
#get 7 companies's stock return 
df_msft=getreturn('MSFT')['ClosePrice'].tolist()
df_mmm=getreturn('MMM')['ClosePrice'].tolist()
df_cat=getreturn('CAT')['ClosePrice'].tolist()
df_ge=getreturn('GE')['ClosePrice'].tolist()
df_utx=getreturn('UTX')['ClosePrice'].tolist()
df_ko=getreturn('KO')['ClosePrice'].tolist()
df_xom=getreturn('XOM')['ClosePrice'].tolist()

g1=np.array([df_ge,df_cat,df_mmm,df_utx])  # companies from same sector
g1=np.rot90(g1)
c1 = np.cov(g1, rowvar=False) 
eig1 = np.linalg.eig(c1)   #[eigvalue eigvector]
eigvalue1=eig1[0]      #eigvalue
print("eigvalue from same sector(GE,CAT,MMM,UTX):",eigvalue1 )
proportion1=[eigvalue1[:]/sum(eigvalue1)] #caculate the proportion of every eigvalue
print("proportion of different eigvalues:",proportion1)
r_reduce1=eig1[1]  #get the eigvector
r_reduce1 = r_reduce1[:,0:3]  #delete the smallest one
norm_g_reduce1 = np.zeros(len(g1) * 4).reshape(len(g1),4)  
for i in range(4):
    norm_g_reduce1[:,i] = g1[:,i] - np.mean(g1[:,i]) 
approx_reduce1 = np.dot(norm_g_reduce1, r_reduce1)   #get new data in three dimensions


g2=np.array([df_msft,df_cat,df_ko,df_xom])  #companies from different sectors
g2=np.rot90(g2)
c2 = np.cov(g2, rowvar=False) 
eig2 = np.linalg.eig(c2)   #[eigvalue eigvector]
eigvalue2=eig2[0]   #get the eigvalue
print("eigvalue from diiferent sectors(MSFT,CAT,KO,XOM):",eigvalue2 )
proportion2=[eigvalue2[:]/sum(eigvalue2)] #caculate the proportion of every eigvalue
print("proportion of different eigvalues:",proportion2)
r_reduce2=eig2[1]  #get the eigvector
r_reduce2 = r_reduce2[:,0:3]  #delete the smallest one
norm_g_reduce2 = np.zeros(len(g2) * 4).reshape(len(g2),4)  
for i in range(4):
    norm_g_reduce2[:,i] = g2[:,i] - np.mean(g2[:,i])  
approx_reduce2 = np.dot(norm_g_reduce2, r_reduce2)   #get new data in three dimensions




























