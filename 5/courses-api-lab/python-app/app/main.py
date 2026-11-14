from fastapi import Depends, FastAPI, HTTPException, Query, status
from sqlalchemy import func, select
from sqlalchemy.orm import Session

from app.database import get_db
from app.models import Course
from app.schemas import CourseCreate, CourseRead, CourseUpdate


app = FastAPI(title="Courses API", version="1.0")


@app.get("/")
def root():
    return {"message": "Courses API працює"}


@app.get("/health")
def health():
    return {"status": "ok"}


@app.get("/courses", response_model=list[CourseRead])
def get_courses(db: Session = Depends(get_db)):
    statement = select(Course).order_by(Course.id)
    return db.scalars(statement).all()


@app.get("/courses/search", response_model=list[CourseRead])
def search_courses(
    title: str = Query(min_length=1),
    db: Session = Depends(get_db),
):
    statement = select(Course).where(Course.title.ilike(f"%{title}%"))
    return db.scalars(statement).all()


# Статичний маршрут розміщуємо перед /courses/{course_id}.
@app.get("/courses/statistics")
def get_course_statistics(db: Session = Depends(get_db)):
    statement = select(
        func.count(Course.id),
        func.coalesce(func.sum(Course.hours), 0),
        func.avg(Course.hours),
        func.min(Course.hours),
        func.max(Course.hours),
    )

    count, total, average, minimum, maximum = db.execute(statement).one()

    return {
        "count": count,
        "total_hours": total,
        "average_hours": float(average) if average is not None else 0,
        "min_hours": minimum,
        "max_hours": maximum,
    }


@app.post(
    "/courses",
    response_model=CourseRead,
    status_code=status.HTTP_201_CREATED,
)
def create_course(data: CourseCreate, db: Session = Depends(get_db)):
    course = Course(**data.model_dump())
    db.add(course)
    db.commit()
    db.refresh(course)
    return course


@app.get("/courses/{course_id}", response_model=CourseRead)
def get_course(course_id: int, db: Session = Depends(get_db)):
    course = db.get(Course, course_id)
    if course is None:
        raise HTTPException(status_code=404, detail="Курс не знайдено")
    return course


@app.put("/courses/{course_id}", response_model=CourseRead)
def update_course(
    course_id: int,
    data: CourseUpdate,
    db: Session = Depends(get_db),
):
    course = db.get(Course, course_id)
    if course is None:
        raise HTTPException(status_code=404, detail="Курс не знайдено")

    for field, value in data.model_dump().items():
        setattr(course, field, value)

    db.commit()
    db.refresh(course)
    return course


@app.delete(
    "/courses/{course_id}",
    status_code=status.HTTP_204_NO_CONTENT,
)
def delete_course(course_id: int, db: Session = Depends(get_db)):
    course = db.get(Course, course_id)
    if course is None:
        raise HTTPException(status_code=404, detail="Курс не знайдено")

    db.delete(course)
    db.commit()