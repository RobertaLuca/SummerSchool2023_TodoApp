namespace TodoApp.Services;

public static class Constants
{
    public static class Messages
    {
        public static string GetMassMessage(string prompt)
        {
            return "Calculate how much these products weight " +
                "[NO OTHER WORDS, NOT A SINGLE WORD PLEASE JUST THE NUMBER AND THAT'S ALL, " +
                $"NO GREETINGS OR A MESSAGE TO TELL THAT YOU UNDERSTOOD THE JOB, JUST THE NUMBER]: {prompt}.";
        }

        public static string GetTaskListMessage(string prompt, int number_of_tasks)
        {
            return "From now on, act as a task generator. " +
                $"Create a list of {number_of_tasks} tasks in order to: {prompt}. " +
                "EACH TASK SHOULD HAVE A TITLE AND A DESCRIPTION. " +
                "FORMAT EXAMPLE:  1. Title: My descriptive title. Description: My short description.";
        }

        public static string GetEstimationMessage(string prompt, DateOnly dateOnly)
        {
            return "[GIVE JUST THE REQUESTED RESPONSE, NOTHING MORE, NO EXPLANATIONS, NO ARGUMENTS, " +
                "JUST THE RESPONSE LIKE IN THE PROVIDED EXAMPLE IN PERGENTAGE] " +
                "[RESPONDE THE QUESTION JUST WITH THE SUCCESS RATE (E.g. 50%)] " +
                $"Do I have enough time to: {prompt}, until {dateOnly}? " +
                $"Today's date is {DateOnly.FromDateTime(DateTime.Now)}.";
        }
    }
}
