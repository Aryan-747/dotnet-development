export function parseServerDate(value?: string | null): Date | null {
  if (!value) {
    return null;
  }

  const trimmed = value.trim();
  if (!trimmed) {
    return null;
  }

  const withTimezone = /([zZ]|[+\-]\d{2}:\d{2})$/.test(trimmed) ? trimmed : `${trimmed}Z`;
  const parsed = new Date(withTimezone);

  if (!Number.isNaN(parsed.getTime())) {
    return parsed;
  }

  const fallback = new Date(trimmed);
  return Number.isNaN(fallback.getTime()) ? null : fallback;
}

export function formatServerDate(value?: string | null): string {
  const parsed = parseServerDate(value);

  if (!parsed) {
    return 'Time unavailable';
  }

  return new Intl.DateTimeFormat('en-IN', {
    dateStyle: 'medium',
    timeStyle: 'short',
  }).format(parsed);
}
