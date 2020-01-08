using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;


public class ForecastTable : DataTable
{
    // An instance of a DataTable with some default columns.  Expressions help quickly calculate E
    public ForecastTable()
    {
        this.Columns.Add("Instance", typeof(Int32));    //The position in which this value occurred in the time-series
        this.Columns.Add("Value", typeof(Decimal));     //The value which actually occurred
        this.Columns.Add("Forecast", typeof(Decimal));  //The forecasted value for this instance



        //E(t) = D(t) - F(t)
        this.Columns.Add("Error", typeof(Decimal), "Forecast-Value");
        //Absolute Error = |E(t)|

        //Percent Error = E(t) / D(t)

        //Absolute Percent Error = |E(t)| / D(t)

    }
}

public class TimeSeries
{
    public const Int32 DEFAULT_IGNORE = 3;
    public const Int32 DEFAULT_HOLDOUT = 6;

    //Bayes: use prior actual value as forecast
    public static ForecastTable naive(decimal[] values, int Extension, int Holdout)
    {
        ForecastTable dt = new ForecastTable();

        for (Int32 i = 0; i < (values.Length + Extension); i++)
        {
            //Insert a row for each value in set
            DataRow row = dt.NewRow();
            dt.Rows.Add(row);

            row.BeginEdit();
            //assign its sequence number
            row["Instance"] = i;

            //if i is in the holdout range of values
            //row["Holdout"] = (i > (values.Length - 1 - Holdout)) && (i < values.Length);

            if (i < values.Length)
            { //processing values which actually occurred
                //Assign the actual value to the DataRow
                row["Value"] = values[i];
                if (i == 0)
                {
                    //first row, value gets itself
                    row["Forecast"] = values[i];
                }
                else
                {
                    //Put the prior row's value into the current row's forecasted value
                    row["Forecast"] = values[i - 1];
                }
            }
            else
            {//Extension rows
                row["Forecast"] = values[values.Length - 1];
            }
            row.EndEdit();
        }
        dt.AcceptChanges();
        return dt;
    }

    //
    //Simple Moving Average
    //
    //            ( Dt + D(t-1) + D(t-2) + ... + D(t-n+1) )
    //  F(t+1) =  -----------------------------------------
    //                              n
    public static ForecastTable simpleMovingAverage(decimal[] values, int Extension, int Periods, int Holdout)
    {
        ForecastTable dt = new ForecastTable();

        for (Int32 i = 0; i < values.Length + Extension; i++)
        {
            //Insert a row for each value in set
            DataRow row = dt.NewRow();
            dt.Rows.Add(row);

            row.BeginEdit();
            //assign its sequence number
            row["Instance"] = i;
            if (i < values.Length)
            {//processing values which actually occurred
                row["Value"] = values[i];
            }

            //Indicate if this is a holdout row


            if (i == 0)
            {//Initialize first row with its own value
                row["Forecast"] = values[i];
            }
            else if (i <= values.Length - Holdout)
            {//processing values which actually occurred, but not in holdout set
                decimal avg = 0;
                DataRow[] rows = dt.Select("Instance>=" + (i - Periods).ToString() + " AND Instance < " + i.ToString(), "Instance");
                foreach (DataRow priorRow in rows)
                {
                    avg += (Decimal)priorRow["Value"];
                }
                avg /= rows.Length;

                row["Forecast"] = avg;
            }
            else
            {//must be in the holdout set or the extension
                decimal avg = 0;

                //get the Periods-prior rows and calculate an average actual value
                DataRow[] rows = dt.Select("Instance>=" + (i - Periods).ToString() + " AND Instance < " + i.ToString(), "Instance");
                foreach (DataRow priorRow in rows)
                {
                    if ((Int32)priorRow["Instance"] < values.Length)
                    {//in the test or holdout set
                        avg += (Decimal)priorRow["Value"];
                    }
                    else
                    {//extension, use forecast since we don't have an actual value
                        avg += (Decimal)priorRow["Forecast"];
                    }
                }
                avg /= rows.Length;

                //set the forecasted value
                row["Forecast"] = avg;
            }
            row.EndEdit();
        }

        dt.AcceptChanges();
        return dt;
    }

    public static ForecastTable wellsweilderMovingaverage(decimal[] values, int Extension, int Periods, int Holdout)
    {
        ForecastTable dt = new ForecastTable();

        for (Int32 i = 0; i < values.Length + Extension; i++)
        {
            //Insert a row for each value in set
            DataRow row = dt.NewRow();
            dt.Rows.Add(row);

            row.BeginEdit();
            //assign its sequence number
            row["Instance"] = i;
            if (i < values.Length)
            {//processing values which actually occurred
                row["Value"] = values[i];
            }

            //Indicate if this is a holdout row


            if (i == 0)
            {//Initialize first row with its own value
                row["Forecast"] = values[i];
            }
            else if (i <= values.Length - Holdout)
            {//processing values which actually occurred, but not in holdout set
                decimal avg = 0;
                DataRow[] rows = dt.Select("Instance>=" + (i - Periods).ToString() + " AND Instance < " + i.ToString(), "Instance");
                foreach (DataRow priorRow in rows)
                {
                    avg += (Decimal)priorRow["Value"];
                }
                avg /= rows.Length;

                row["Forecast"] = 2 / avg + 1;
            }
            else
            {//must be in the holdout set or the extension
                decimal avg = 0;

                //get the Periods-prior rows and calculate an average actual value
                DataRow[] rows = dt.Select("Instance>=" + (i - Periods).ToString() + " AND Instance < " + i.ToString(), "Instance");
                foreach (DataRow priorRow in rows)
                {
                    if ((Int32)priorRow["Instance"] < values.Length)
                    {//in the test or holdout set
                        avg += (Decimal)priorRow["Value"] + 1 / 2;
                    }
                    else
                    {//extension, use forecast since we don't have an actual value
                        avg += (Decimal)priorRow["Forecast"] + 2 / avg + 1;
                    }
                }
                avg /= rows.Length;

                //set the forecasted value
                row["Forecast"] = avg;
            }
            row.EndEdit();
        }

        dt.AcceptChanges();
        return dt;
    }
    //
    //Weighted Moving Average
    //            
    //  F(t+1) =  (Weight1 * D(t)) + (Weight2 * D(t-1)) + (Weight3 * D(t-2)) + ... + (WeightN * D(t-n+1))
    //          
    public static ForecastTable weightedMovingAverage(decimal[] values, int Extension, params decimal[] PeriodWeight)
    {
        //PeriodWeight[].Length is used to determine the number of periods over which to average
        //PeriodWeight[x] is used to apply a weight to the prior period's value

        //Make sure PeriodWeight values add up to 100%
        decimal test = 0;
        foreach (decimal weight in PeriodWeight)
        {
            test += weight;
        }
        if (test != 1)
        {
            
            return null;
        }

        ForecastTable dt = new ForecastTable();

        for (Int32 i = 0; i < values.Length + Extension; i++)
        {
            //Insert a row for each value in set
            DataRow row = dt.NewRow();
            dt.Rows.Add(row);

            row.BeginEdit();
            //assign its sequence number
            row["Instance"] = i;

            if (i < values.Length)
            {//we're in the test set
                row["Value"] = values[i];
            }

            if (i == 0)
            {//initialize forecast with first row's value
                row["Forecast"] = values[i];
            }
            else if ((i < values.Length) && (i < PeriodWeight.Length))
            {//processing one of the first rows, before we've advanced enough to properly weight past rows
                decimal avg = 0;

                //Get the datarows representing the values within the WMA length
                DataRow[] rows = dt.Select("Instance>=" + (i - PeriodWeight.Length).ToString() + " AND Instance < " + i.ToString(), "Instance");
                for (int j = 0; j < rows.Length; j++)
                {//apply an initial, uniform weight (1 / rows.Length) to the initial rows
                    avg += (Decimal)rows[j]["Value"] * (1 / rows.Length);
                }
                row["Forecast"] = avg;
            }
            else if ((i < values.Length) && (i >= PeriodWeight.Length))
            {//Out of initial rows and processing the test set
                decimal avg = 0;

                //Get the rows within the weight range just prior to the current row
                DataRow[] rows = dt.Select("Instance>=" + (i - PeriodWeight.Length).ToString() + " AND Instance < " + i.ToString(), "Instance");
                for (int j = 0; j <= rows.Length - 1; j++)
                {//Apply the appropriate period's weight to the value
                    avg += (Decimal)rows[j]["Value"] * PeriodWeight[j];
                }
                //Assign the forecasted value to the current row
                row["Forecast"] = avg;
            }
            else
            {//into the extension
                decimal avg = 0;

                DataRow[] rows = dt.Select("Instance>=" + (i - PeriodWeight.Length).ToString() + " AND Instance < " + i.ToString(), "Instance");
                for (int j = 0; j < rows.Length; j++)
                {//with no actual values to weight, use the previous rows' forecast instead
                    avg += (Decimal)rows[j]["Forecast"] * PeriodWeight[j];
                }
                row["Forecast"] = avg;
            }
            row.EndEdit();
        }

        dt.AcceptChanges();
        return dt;
    }

    //
    //Exponential Smoothing
    //
    //  F(t+1) =    ( Alpha * D(t) ) + (1 - Alpha) * F(t)
    //
    public static ForecastTable exponentialSmoothing(decimal[] values, int Extension, decimal Alpha)
    {
        ForecastTable dt = new ForecastTable();
        for (Int32 i = 0; i < (values.Length + Extension); i++)
        {
            //Insert a row for each value in set
            DataRow row = dt.NewRow();
            dt.Rows.Add(row);

            row.BeginEdit();
            //assign its sequence number
            row["Instance"] = i;
            if (i < values.Length)
            {//test set
                row["Value"] = values[i];
                if (i == 0)
                {//initialize first value
                    row["Forecast"] = values[i];
                }
                else
                {
                    //main area of forecasting
                    DataRow priorRow = dt.Select("Instance=" + (i - 1).ToString())[0];
                    decimal PriorForecast = (Decimal)priorRow["Forecast"];
                    decimal PriorValue = (Decimal)priorRow["Value"];

                    row["Forecast"] = PriorForecast + (Alpha * (PriorValue - PriorForecast));
                }
            }
            else
            {//extension has to use prior forecast instead of prior value
                DataRow priorRow = dt.Select("Instance=" + (i - 1).ToString())[0];
                decimal PriorForecast = (Decimal)priorRow["Forecast"];
                decimal PriorValue = (Decimal)priorRow["Forecast"];

                row["Forecast"] = PriorForecast + (Alpha * (PriorValue - PriorForecast));
            }
            row.EndEdit();
        }

        dt.AcceptChanges();

        return dt;
    }

    //
    // Adaptive Rate Smoothing
    //
    public static ForecastTable adaptiveRateSmoothing(decimal[] values, int Extension, decimal MinGamma, decimal MaxGamma)
    {
        ForecastTable dt = new ForecastTable();

        for (Int32 i = 0; i < (values.Length + Extension); i++)
        {
            //Insert a row for each value in set
            DataRow row = dt.NewRow();
            dt.Rows.Add(row);

            row.BeginEdit();
            //assign its sequence number
            row["Instance"] = i;
            if (i < values.Length)
            {
                row["Value"] = values[i];
                if (i == 0)
                {//initialize first row
                    row["Forecast"] = values[i];
                }
                else
                {//calculate gamma and forecast value
                    DataRow priorRow = dt.Select("Instance=" + (i - 1).ToString())[0];
                    decimal PriorForecast = (Decimal)priorRow["Forecast"];
                    decimal PriorValue = (Decimal)priorRow["Value"];

                    //decimal Gamma = Math.Abs(TrackingSignal(dt, 3));
                    //if (Gamma < MinGamma)
                    //    Gamma = MinGamma;
                    //if (Gamma > MaxGamma)
                    //    Gamma = MaxGamma;

                    //row["Forecast"] = PriorForecast + (Gamma * (PriorValue - PriorForecast));
                }
            }
            else
            {//extension set, can't use actual values anymore
                DataRow priorRow = dt.Select("Instance=" + (i - 1).ToString())[0];
                decimal PriorForecast = (Decimal)priorRow["Forecast"];
                decimal PriorValue = (Decimal)priorRow["Forecast"];

                //decimal Gamma = Math.Abs(TrackingSignal(dt, 3));
                //if (Gamma < MinGamma)
                //    Gamma = MinGamma;
                //if (Gamma > MaxGamma)
                //    Gamma = MaxGamma;

                //row["Forecast"] = PriorForecast + (Gamma * (PriorValue - PriorForecast));
            }
            row.EndEdit();
        }

        dt.AcceptChanges();

        return dt;

    }

    #region "Forecast Performance Measures"

    //MeanSignedError = Sum(E(t)) / n
    public static decimal MeanSignedError(ForecastTable dt, int IgnoreInitial)
    {
        string Filter = "Error Is Not Null AND Instance > " + IgnoreInitial.ToString();


        if (dt.Select(Filter).Length == 0)
            return 0;
        return (Decimal)dt.Compute("Avg(Error)", Filter);
    }

    //MeanAbsoluteError = Sum(|E(t)|) / n


    //MeanPercentError = Sum( PercentError ) / n
    public static decimal MeanPercentError(ForecastTable dt, int IgnoreInitial)
    {
        string Filter = "PercentError Is Not Null AND Instance > " + IgnoreInitial.ToString();


        if (dt.Select(Filter).Length == 0)
            return 0;
        return (Decimal)dt.Compute("Avg(PercentError)", Filter);
    }

    //MeanAbsolutePercentError = Sum( |PercentError| ) / n
    public static decimal MeanAbsolutePercentError(ForecastTable dt, int IgnoreInitial)
    {
        string Filter = "AbsolutePercentError Is Not Null AND Instance > " + IgnoreInitial.ToString();


        if (dt.Select(Filter).Length == 0)
            return 1;
        return (Decimal)dt.Compute("AVG(AbsolutePercentError)", Filter);
    }



    //MSE = Sum( E(t)^2 ) / n
    public static decimal MeanSquaredError(ForecastTable dt, int IgnoreInitial, int DegreesOfFreedom)
    {
        decimal SquareError = 0;
        string Filter = "Error Is Not Null AND Instance > " + IgnoreInitial.ToString();


        DataRow[] rows = dt.Select(Filter);
        if (rows.Length == 0)
            return 0;

        foreach (DataRow row in rows)
        {
            SquareError = (Decimal)Math.Pow(Double.Parse(row["Error"].ToString()), (Double)2.0);
        }
        return SquareError / (dt.Rows.Count - DegreesOfFreedom);
    }

    //CumulativeSignedError = Sum( E(t) )
    public static decimal CumulativeSignedError(ForecastTable dt, int IgnoreInitial)
    {
        string Filter = "Error Is Not Null AND Instance > " + IgnoreInitial.ToString();

        if (dt.Select(Filter).Length == 0)
            return 0;
        return (Decimal)dt.Compute("SUM(Error)", Filter);
    }

    //CumulativeAbsoluteError = Sum( |E(t)| )
    public static decimal CumulativeAbsoluteError(ForecastTable dt, int IgnoreInitial)
    {
        string Filter = "AbsoluteError Is Not Null AND Instance > " + IgnoreInitial.ToString();


        if (dt.Select(Filter).Length == 0)
            return 0;
        return (Decimal)dt.Compute("SUM(AbsoluteError)", Filter);
    }

    #endregion //Forecast Performance Measures

}