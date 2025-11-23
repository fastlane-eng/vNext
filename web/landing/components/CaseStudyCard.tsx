import React from 'react';

interface CaseStudy {
  title: string;
  description: string;
  imageUrl: string;
}

interface CaseStudyCardProps {
  caseStudy: CaseStudy;
}

export const CaseStudyCard: React.FC<CaseStudyCardProps> = ({ caseStudy }) => {
  return (
    <div className="p-4 border rounded-lg flex flex-col items-center">
      <img src={caseStudy.imageUrl} alt={caseStudy.title} className="w-full h-32 object-cover rounded" />
      <h3 className="text-lg font-semibold mt-3">{caseStudy.title}</h3>
      <p className="text-sm text-gray-600 mt-1">{caseStudy.description}</p>
    </div>
  );
};
