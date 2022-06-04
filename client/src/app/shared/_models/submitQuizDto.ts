import { result } from "./question-result"

export interface submitQuizDto {
    questionResult: result[],
    quizId: number,
    userName: string
}