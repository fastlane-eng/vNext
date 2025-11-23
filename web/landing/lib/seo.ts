export function generateMetaTags({
  title,
  description,
  url,
}: {
  title: string;
  description: string;
  url: string;
}) {
  return [
    { name: 'title', content: title },
    { name: 'description', content: description },
    { property: 'og:title', content: title },
    { property: 'og:description', content: description },
    { property: 'og:url', content: url },
  ];
}
