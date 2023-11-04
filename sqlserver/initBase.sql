USE [master]
CREATE DATABASE FibonacciData
GO

USE FibonacciData
GO

CREATE SCHEMA sch_fib AUTHORIZATION [dbo];
GO

CREATE TABLE [sch_fib].[T_Fibonacci] (
    [FIB_Id] uniqueidentifier NOT NULL DEFAULT ((newid())),
    [FIB_Input] int NOT NULL,
    [FIB_Output] bigint NOT NULL,
    [FIB_CreatedTimestamp] datetime2 NOT NULL DEFAULT (('0001-01-01T00:00:00.0000000')),
    [FibLastExecutionTimestamp] datetime2 NOT NULL,
    CONSTRAINT [PK_Fibonacci] PRIMARY KEY ([FIB_Id])
    );