#!/bin/bash

# Add all changes
git add .

# Ask for a commit message
echo "Enter the commit message: "
read commitMessage

# Commit the changes
git commit -m "$commitMessage"

# Get the current branch name
currentBranch=$(git rev-parse --abbrev-ref HEAD)

# Ask for confirmation before pushing
echo "Do you want to push to '$currentBranch'? (y/n)"
read confirm

if [[ "$confirm" == "y" || "$confirm" == "Y" ]]; then
    git push origin "$currentBranch"
    echo "✅ Changes pushed to '$currentBranch'"
else
    echo "❌ Push canceled."
fi