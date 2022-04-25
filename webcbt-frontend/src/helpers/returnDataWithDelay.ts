const returnDataWithDelay = <T>(
  data: T,
  throttling?: '4G' | 'fast 3G' | 'slow 3G',
): Promise<T> => {
  let delayMs = 0;

  switch (throttling) {
    case 'slow 3G':
      delayMs = Math.floor(Math.random() * 10000) + 3000;
      break;
    case 'fast 3G':
      delayMs = Math.floor(Math.random() * 3000) + 500;
      break;
    case '4G':
    default:
      delayMs = Math.floor(Math.random() * 500) + 100;
      break;
  }

  return new Promise((resolve, reject) =>
    setTimeout(() => resolve(data), delayMs),
  );
};

export default returnDataWithDelay;
