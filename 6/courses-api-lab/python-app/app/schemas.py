from pydantic import BaseModel, ConfigDict, Field


class CourseCreate(BaseModel):
    title: str = Field(min_length=1, max_length=200)
    teacher: str = Field(min_length=1, max_length=150)
    hours: int = Field(gt=0, le=1000)
    description: str | None = Field(default=None, max_length=500)


class CourseUpdate(BaseModel):
    title: str = Field(min_length=1, max_length=200)
    teacher: str = Field(min_length=1, max_length=150)
    hours: int = Field(gt=0, le=1000)
    description: str | None = Field(default=None, max_length=500)


class CourseRead(BaseModel):
    model_config = ConfigDict(from_attributes=True)
    description: str | None = None

    id: int
    title: str
    teacher: str
    hours: int