/*
* Author: Adam Shapira; 3160044809
 */
#ifndef I_TIMESERIES_H_EXPORT
#define I_TIMESERIES_H_EXPORT

#ifdef I_TIMESERIES_H_EXPORT // defined only in the exporting DLL project
#define TIMESERIES_API __declspec(dllexport)
#else
#define TIMESERIES_API __declspec(dllimport)
#endif

#include "pch.h"
#include <vector>
#include <string>

class ITimeSeries {

public:
	// making sure that there will be a constructor that get both the csv and the feature names  
	ITimeSeries(std::string csv, std::vector<std::string> featureNames) {}

	/**
	*
	* @param feature - a string which is a name of a feature in the data
	* @return  a vector with all the the data of the feature
	*/
	virtual const std::vector<float>& getFeatureData(const std::string& feature) const = 0;

	/**
	*
	* @return a vector of all the feature name in the same order as the headers from the csv
	*/
	virtual const std::vector<std::string>& getFeatureNames() const = 0;

	virtual int getFetureindex(std::string name) const = 0;

	virtual ~ITimeSeries() = default;
};
extern "C"	TIMESERIES_API ITimeSeries * getTimeSeries(char* csv, char* featureNames[], int numOfFeaturs);
#endif /* TIMESERIES_H_ */
