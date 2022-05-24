type userId = 0 | 1;

export type MoodTestCategory =
  | 'Depression'
  | 'Anxiety'
  | 'Addictions'
  | 'Anger'
  | 'Relationships'
  | 'Happiness';

export default interface MoodTest {
  question1: number;
  question2: number;
  question3: number;
  question4: number;
  question5: number;
}

export interface MoodTestRequest extends MoodTest {
  userId: userId;
  category: MoodTestCategory;
}
