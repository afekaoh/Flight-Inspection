

#ifndef ANOMALYDETECTOR_H_
#define ANOMALYDETECTOR_H_
#include "pch.h"
#include <vector>
#include "ITimeSeries.h"

using namespace std;


struct AnomalyReport {
	int featureName1;
	int featureName2;
	int timeStep;

	AnomalyReport(int feature1, int feature2, int time) : featureName1(feature1), featureName2(feature2), timeStep(time) {}
};

class TimeSeriesAnomalyDetector {
protected:
	float correlationThreshold = 0.5;

public:

	virtual void learnNormal(ITimeSeries* its) = 0;

	virtual std::vector<AnomalyReport> detect(ITimeSeries* its) = 0;

	virtual ~TimeSeriesAnomalyDetector() = default;

	virtual void setCorrelationThreshold(float threshold) = 0;

	virtual float getCorrelationThreshold() const = 0;
};

extern "C" TIMESERIES_API TimeSeriesAnomalyDetector* getAnomalyDetector();


#endif /* ANOMALYDETECTOR_H_ */
