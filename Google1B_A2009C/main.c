#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <stdbool.h>

struct node
{
	double prob;
	char name[12];
	struct node* left;
	struct node* right;
};

#define LINE_BUFFER 81

char* read_text(FILE* file, int* lines);
void nextToken(char* text, char* token, int* parsepos);
struct node* parse_tree(char* text, int* text_pos);
double calcprob(struct node* n, char** features_arr, int features_size);
void free_tree(struct node* n);

int main(int argc, char* argv[])
{
	FILE* input_file = fopen("small.in", "r");
	FILE* output_file = fopen("out.txt", "w");

	char buffer[LINE_BUFFER];
	fgets(buffer, LINE_BUFFER, input_file); //read number of cases
	int cases = atoi(buffer);

	for (int i = 1; i <= cases; i++)
	{
		int lines;
		int pos = 0;
		char* text;

		fprintf(output_file,"Case #%d:\n", i);
		text = read_text(input_file,&lines); // read tree text

		pos = 0;
		struct node* root = parse_tree(text, &pos); //Parse the tree

		free(text); //free the treetext

		text = read_text(input_file,&lines);

		pos = 0;
		for (int j = 0; j < lines; j++)
		{			
			nextToken(text, buffer, &pos);

			nextToken(text, buffer, &pos);
			int f = atoi(buffer);

			char** features_arr = malloc(f * sizeof(char*));
			for (int k = 0; k < f; k++)
			{
				features_arr[k] = malloc(LINE_BUFFER * sizeof(char));
			}

			for (int k = 0; k < f; k++)
			{
				nextToken(text, buffer, &pos);
				strcpy(features_arr[k], buffer);
			}
			fprintf(output_file,"%.7lf\n", calcprob(root, features_arr, f));

			

			//free the features 2D array
			for (int k = 0; k < f; k++)
			{
				free(features_arr[k]);
			}
			free(features_arr);
		}
		free(text); //free the animal text

		free_tree(root); //free the tree

	}
	
}

char* read_text(FILE* file,int* lines)
{
	char line_buffer[LINE_BUFFER];

	fgets(line_buffer, LINE_BUFFER, file);
	*lines = atoi(line_buffer);

	char* textarr = malloc( (*lines) * LINE_BUFFER *sizeof(char));
	if (textarr == NULL)
	{
		fputs("Something went wrong", stderr);
		exit(-1);
	}
	textarr[0] = '\0'; //To make strcat work

	for (int i = 0; i < *lines; i++)
	{
		fgets(line_buffer, LINE_BUFFER, file);
		strcat(textarr, line_buffer);
	}

	return textarr;
}

void nextToken(char* text, char* token, int* parsepos)
{
	int pos = *parsepos;
	
	int tokenpos = 0;

	while (true)
	{
		char c = text[pos];

		if (c == ' ' || c == '\n')
		{
			if (tokenpos > 0) //If we have wriiten data and wee see whitespace then it's the end of the token
			{
				token[tokenpos] = '\0';

				pos++;
				*parsepos = pos;
				return;
			}
			else
			{
				pos++;
				//Keep going
			}			
		}
		else if (c == '(' || c == ')')
		{
			if (tokenpos == 0) //if nothing is wriiten then this is the new token
			{
				token[0] = c;
				token[1] = '\0';

				pos++;
				*parsepos = pos;
				return;
			}
			else //else we store the previous token and this will be stored next
			{
				token[tokenpos] = '\0';

				*parsepos = pos;
				return;
			}
		}
		else //It is a letter or number
		{
			token[tokenpos++] = c;
			pos++;
		}
	}

}

struct node* parse_tree(char* text, int* text_pos)
{	
	char token[81];

	nextToken(text, token, text_pos);

	nextToken(text, token, text_pos);
	double prob = atof(token);

	struct node* newNode = malloc(sizeof(struct node));
	if (newNode == NULL)
	{
		fputs("Something went wrong", stderr);
		exit(-1);
	}

	//Initiallization
	newNode->left = NULL;
	newNode->right = NULL;
	strcpy(newNode->name, "");


	newNode->prob = prob;

	nextToken(text, token, text_pos);
	if (strcmp(token, ")") == 0)
		return newNode;
		
	strcpy(newNode->name, token);

	newNode->left = parse_tree(text, text_pos);

	newNode->right = parse_tree(text, text_pos);
	nextToken(text, token, text_pos);

	return newNode;
}

double calcprob(struct node* n, char** features_arr, int features_size)
{
	if (strcmp(n->name, "") == 0)
		return n->prob;
	else
	{
		bool flag = false;
		for (int i = 0; i < features_size; i++)
		{
			if (strcmp(n->name, features_arr[i]) == 0)
			{
				flag = true;
				break;
			}
		}

		if (flag)
			return (n->prob) * calcprob(n->left, features_arr, features_size);
		else
			return (n->prob) * calcprob(n->right, features_arr, features_size);
	}
}

void free_tree(struct node* n)
{
	if (n == NULL)
		return;	

	free_tree(n->left);
	free_tree(n->right);
	free(n);
}
