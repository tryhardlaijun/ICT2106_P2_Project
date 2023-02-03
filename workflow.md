# Git Workflow

## Step 1: Creating your branch

When starting a new task, **ALWAYS** branch off main and name your branch accordingly:

- feature: if you're adding a new feature to the repository  
- bug: if you're fixing a bug that was discovered  
- chore: if you're adding something to the repository that is not related to features or bug fixes  

So if you're adding a login page, you should name your branch `feature/add-login-page`

In your terminal, you can create a new branch and work on it like so:  

```console
git branch feature/add-login-page
git checkout feature/add-login-page
```

## Step 2: Working on your branch

✅ Regularly commit your changes with meaningful commit messages  
✅ Regularly ensure that errors and warnings are resolved before committing.

## Step 3: Done with your work

✅ Build and compile your code to make sure its compilable  
✅ Prepare to merge your changes into the repository

## Step 4: Creating a Pull Request

***Nice*** now you are all done with your work, follow these steps to create a Pull Request (PR) and merge into the main branch:

- Go to your module repository on GitHub
- Click the "New pull request" button
- Choose your branch as the source branch and the main branch as the destination branch
- Fill out the PR template auto-generated

- Get at least two approvals from the following code reviewers:  

|Module 1| Module 2| Module 3|
|:---:|:---:|:---:|
|@fabianchua6 (FE)|@bennylim0926|@rawsashimi1604|
|@doublebounce (FE/BE)  |@JiaJun8899|@rb-t|
|@JustBrandonLim (FE/BE)  |@jzlong99|@jianwei|
|@pangkaho14 (BE/DE)  |@maximus|-|

## Step 5: Merge and Clean Up

Once your PR is approved and merged into the main branch

✅ Delete your branch from the repository to keep it organized
