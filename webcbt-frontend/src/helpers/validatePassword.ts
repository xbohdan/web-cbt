const validatePassword = (password: string): Promise<string | void> => {
  if (password.length < 6) {
    return Promise.reject(
      new Error('Password must have at least 6 characters'),
    );
  } else if (password.toUpperCase() === password) {
    return Promise.reject(
      new Error('Password must have at least one lowercase character'),
    );
  } else if (password.toLowerCase() === password) {
    return Promise.reject(
      new Error('Password must have at least one uppercase character'),
    );
  } else if (!/\d/.test(password)) {
    return Promise.reject(new Error('Password must have at least one digit'));
  } else if (!/[^a-zA-Z0-9]/.test(password)) {
    return Promise.reject(
      new Error('Password must have at least one non-alphanumeric character'),
    );
  } else {
    return Promise.resolve();
  }
};

export default validatePassword;
