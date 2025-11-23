import React from 'react';

interface Kpi {
  title: string;
  value: string;
  unit?: string;
}

interface KpiStripProps {
  kpis: Kpi[];
}

export const KpiStrip: React.FC<KpiStripProps> = ({ kpis }) => {
  return (
    <div className="flex space-x-4">
      {kpis.map((kpi, index) => (
        <div key={index} className="flex flex-col items-center">
          <div className="text-2xl font-bold">{kpi.value}</div>
          {kpi.unit && <div className="text-sm text-gray-500">{kpi.unit}</div>}
          <div className="text-sm">{kpi.title}</div>
        </div>
      ))}
    </div>
  );
};
