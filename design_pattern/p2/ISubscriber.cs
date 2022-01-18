using System;
using System.Collections.Generic;
using System.Text;

namespace Leetcode.design_pattern.p2
{
    public interface ISubscriber
    {
        void OnMeasurementsChanged();
    }
}
