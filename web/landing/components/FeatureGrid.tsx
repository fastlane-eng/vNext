import React from 'react';

interface Feature {
  title: string;
  description: string;
}

interface FeatureGridProps {
  features: Feature[];
}

export const FeatureGrid: React.FC<FeatureGridProps> = ({ features }) => {
  return (
    <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
      {features.map((feature, index) => (
        <div key={index} className="p-4 border rounded-lg">
          <h3 className="text-lg font-semibold mb-2">{feature.title}</h3>
          <p className="text-sm text-gray-600">{feature.description}</p>
        </div>
      ))}
    </div>
  );
};
