import {MoodTestCategory, MoodTestRequest} from '../types/MoodTest';

const NUM_OF_TESTS = 50;

const getRandomInt = (min: number, max: number): number => {
  return Math.floor(Math.random() * (max - min) + min); //The maximum is exclusive and the minimum is inclusive
};

const getRandomId = (): number => {
  return getRandomInt(0, 5000);
};

const moodTestCategories = [
  'Depression',
  'Anxiety',
  'Addictions',
  'Anger',
  'Relationships',
  'Happiness',
];

const getRandomCategory = (): MoodTestCategory => {
  return moodTestCategories[getRandomInt(0, 6)] as MoodTestCategory;
};

let mockMoodTests: MoodTestRequest[] = [];

for (let i = 0; i < NUM_OF_TESTS; i++) {
  mockMoodTests.push({
    userId: getRandomId(),
    evaluationId: getRandomId(),
    category: getRandomCategory(),
    question1: getRandomInt(1, 6),
    question2: getRandomInt(1, 6),
    question3: getRandomInt(1, 6),
    question4: getRandomInt(1, 6),
    question5: getRandomInt(1, 6),
  });
}

export default mockMoodTests;
