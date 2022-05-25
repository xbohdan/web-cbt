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
  userId: number;
  category: MoodTestCategory;
}
