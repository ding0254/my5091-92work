import quandl 
import numpy as np 
import pandas as pd
import pyodbc 
quandl.ApiConfig.api_key = "7kaiDSAcK7WRa_1Hypav" 
def quandldata(ticker):    #function to import data from quandl
    quandl.ApiConfig.api_key = "7kaiDSAcK7WRa_1Hypav" 
    df=pd.DataFrame(quandl.get('EOD/'+ticker))
    df=df.reset_index()
    return df
def add_name(company, ticker, market):   #insert data into the table instrument from sql
    conn = pyodbc.connect('Driver={SQL Server};' 'Server=LAPTOP-9LI79KT8;' 'Database=MFM_Financial;' 'Trusted_Connection=yes;')
    cursor = conn.cursor()
    cursor.execute("insert into [MFM_Financial].[FinData].[Instrument] values ('" + company + "','" + ticker + "','" + market + "')") 
    conn.commit()
#select 7 companies to insert into the table instrument in sql
add_name('Microsoft','MSFT','NASDAQ')
add_name('General Electric','GE','NYSE')
add_name('Caterpillar','CAT','NYSE')
add_name('MMM','MMM','NYSE')
add_name('United Technologies Corp','UTX','NYSE')
add_name('Coca-Cola Corp','KO','NYSE')
add_name('Exxon Mobil','XOM','NYSE')
def get_name_id(ticker):   #function to get the id from table of instrument in sql using the ticker
    conn = pyodbc.connect('Driver={SQL Server};' 'Server=LAPTOP-9LI79KT8;' 'Database=MFM_Financial;' 'Trusted_Connection=yes;')
    cursor = conn.cursor()
    cursor.execute("select ID from [MFM_Financial].[FinData].[Instrument] where StockTicker='" + ticker + "'") 
    return cursor.fetchone()
def add_timeseries_from_api(ticker):   #insert all data from quandl with the given ticker to the table of HistPrices in sql
    tickerid = get_name_id(ticker)   #get the ticker id
    conn = pyodbc.connect('Driver={SQL Server};' 'Server=LAPTOP-9LI79KT8;' 'Database=MFM_Financial;' 'Trusted_Connection=yes;')
    cursor = conn.cursor()
    df = quandldata(ticker)
    for i in range(len(df)): 
        cursor.execute("insert into [MFM_Financial].[FinData].[HistPrices] values  ('" + str(tickerid[0]) + "','" +str(df.iloc[i]['Date']) + "',  '"  + str(df.iloc[i]['Open']) + "','" + str(df.iloc[i]['High']) +"','"+ str(df.iloc[i]['Low']) +"','" + str(df.iloc[i]['Close']) + "','" + str(int(df.iloc[i]['Volume'])) + "')")
    conn.commit()
add_timeseries_from_api("MSFT")
add_timeseries_from_api("GE")
add_timeseries_from_api("CAT")
add_timeseries_from_api("MMM")
add_timeseries_from_api("UTX")
add_timeseries_from_api("KO")
add_timeseries_from_api("XOM")


